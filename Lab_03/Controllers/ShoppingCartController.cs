using Lab_03.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lab_03.Data;
using Lab_03.Repository;
using Lab_03.Helper;



namespace Lab_03.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IProductRepository productRepository, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _productRepository = productRepository;
            _context = context;
            _userManager = userManager;
        }

        //Thêm 1 sản phẩm vào giỏ hàng
        public async Task<IActionResult> AddToCartAsync(int productId, int quantity)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
            Product product = await GetProductFromDatabaseAsync(productId);
            if (product != null)
            {
                var imageUrl = product.ImageUrl ?? "/images/default-product-image.png";

                var cartItem = new CartItem
                {
                    Id = productId,
                    Name = product.Name,
                    ImageUrl = imageUrl,
                    Price = product.Price,
                    Quantity = quantity
                };
                //Lấy giỏ hàng hiện tại từ session của người dùng. Nếu không có giỏ hàng nào được tìm thấy, tạo một giỏ hàng mới.
                var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
                cart.AddItem(cartItem); //Thêm 1 sản phẩm vào giỏ hàng

                HttpContext.Session.SetObjectAsJson("Cart", cart); // Lưu giỏ hàng mới vào session của người dùng.

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Product");
        }

        

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        // Các actions khác...

        private async Task<Product> GetProductFromDatabaseAsync(int productId)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm

            return await _productRepository.GetByIdAsync(productId);
        }


        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.RemoveItem(productId);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                TempData["CartEmptyMessage"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("Index");
            }
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart"); //lấy đối tượng giỏ hàng từ session 
            if (cart == null || !cart.Items.Any())
            {
                TempData["CartEmptyMessage"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.GetUserAsync(User);
            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
            order.OrderDetails = cart.Items.Select(i => new OrderDetail
            {
                ProductId = i.Id,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart");

            return View("OrderCompleted", order.Id); // Trang xác nhận hoàn thành đơn hàng
        }
        public async Task<IActionResult> UpdateQuantityAsync(int productId, int quantity)
        {
        //    Nếu không có đối tượng giỏ hàng trong phiên, nó tạo một đối tượng ShoppingCart mới.
        //    Sau đó, nó cập nhật số lượng của sản phẩm cụ thể trong giỏ hàng bằng cách sử dụng
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.UpdateQuantity(productId, quantity);
            HttpContext.Session.SetObjectAsJson("Cart", cart); //lưu đối tượng giỏ hàng đã cập nhật trở lại phiên sử dụng 
            return RedirectToAction("Index");
        }

    }


    
}

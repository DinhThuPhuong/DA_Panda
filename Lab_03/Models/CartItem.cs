namespace Lab_03.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string? ImageUrl { get; set; }
        public List<ProductImage>? Images { get; set; }
    }

}

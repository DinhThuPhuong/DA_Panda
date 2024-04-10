using Lab_03.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Lab_03.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> CreateAdminAccount()
        {
            if(!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var user = new IdentityUser
            {
                UserName = "Admin@gmail.com",
                Email = "Admin@gmail.com"
            };
            var result = await _userManager.CreateAsync(user, "Abc@123");
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return Content("Admin Account Created Successfully!");
            }
            return BadRequest("Failed to Create Admin Account");
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

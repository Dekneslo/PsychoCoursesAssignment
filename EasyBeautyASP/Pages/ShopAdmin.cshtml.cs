using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EasyBeautyASP.Pages
{
    [Authorize(Roles = "Admin")]
    public class ShopAdminModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;        

        public IList<ApplicationUser> Users { get; set; }
        public List<Product> Products { get; set; }

        public readonly ApplicationDbContext _db;

        public ShopAdminModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public void OnGet()
        {
            Products = _db.Products.ToList();
            Users = _userManager.Users.ToList();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    
                }
                else
                {
                    
                }
            }
            else
            {
                
            }

            return RedirectToPage("/ShopAdmin");
        }
    }
}

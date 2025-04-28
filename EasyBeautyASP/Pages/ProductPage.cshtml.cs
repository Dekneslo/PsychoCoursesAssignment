using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyBeautyASP.Models;


namespace EasyBeautyASP.Pages
{
    public class ProductPageModel : PageModel
    {
        private readonly ApplicationDbContext _db;        
        public ProductPageModel(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public Product Products { get; set; }  
        
        public IActionResult OnGet(int id)
        {
            Products = _db.Products.Find(id);

            if (Products == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId, int quantity, string userId)
        {

            var product = _db.Products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            var user = _db.Users.FirstOrDefault(u => u.UserName == userId);

            if (user == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                Product = product,
                Quantity = quantity,
                UserName = userId
            };

            _db.CartItems.Add(cartItem);
            _db.SaveChanges();            

            return RedirectToPage("/Cart");


        }
    }
}

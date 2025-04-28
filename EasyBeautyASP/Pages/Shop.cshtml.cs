using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyBeautyASP.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyBeautyASP.Pages
{
    public class ShopModel : PageModel

    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<Product> Products { get; set; }

        public readonly ApplicationDbContext _db;

        public ShopModel(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task OnGetAsync(string SearchTerm)
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Products = await _db.Products
                    .Where(p => p.ProductName.Contains(SearchTerm) || p.ProductFirstDescription.Contains(SearchTerm))
                    .ToListAsync();
            }
            else
            {
                Products = await _db.Products.ToListAsync();
            }
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

            var searchTerm = Request.Query["searchTerm"].ToString();
            await OnGetAsync(searchTerm);

            return RedirectToPage("/Cart");
            
            
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyBeautyASP.Pages
{
    public class FakePaymentModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public int TotalPrice => CartItems.Sum(item => item.Product.ProductPrice * item.Quantity);

        public List<CartItem> CartItems { get; set; }

        public FakePaymentModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("Cart");

            CartItems = await _db.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserName == user.UserName) // исправлено здесь
                .ToListAsync();

            if (CartItems == null || !CartItems.Any())
                return RedirectToPage("Cart");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("Cart");

            var cartItems = await _db.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserName == user.UserName) // исправлено здесь
                .ToListAsync();

            foreach (var item in cartItems)
            {
                var alreadyOwned = await _db.UserCourses.AnyAsync(x =>
                    x.UserId == user.Id && x.ProductId == item.ProductId);

                if (!alreadyOwned)
                {
                    _db.UserCourses.Add(new UserCourse
                    {
                        UserId = user.Id,
                        ProductId = item.ProductId
                    });
                }

                _db.CartItems.Remove(item);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage("MyCourses");
        }
    }
}

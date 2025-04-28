using System.Threading.Tasks;
using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EasyBeautyASP.Pages
{
    public class CartModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CartModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            
            _userManager = userManager;
            _context = context;
        }

        public int ShoppingCartTotal => CartItems.Sum(item => item.Product.ProductPrice * item.Quantity);

        public List<CartItem> CartItems { get; set; }

        public async Task LoadCartItemsAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            CartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserName == user.UserName)
                .ToListAsync();
        }

        public async Task OnGetAsync()
        {
            await LoadCartItemsAsync();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);            
            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.UserName == user.Id && c.ProductId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserName = user.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }

            await LoadCartItemsAsync();

            await _context.SaveChangesAsync();

            return RedirectToPage("/Cart");
        }

        public async Task<IActionResult> OnPostUpdateCart(int cartItemId, int quantity)
        {
            var cartItems = await _context.CartItems.FindAsync(cartItemId);

            if (cartItems == null)
            {
                return NotFound();
            }

            cartItems.Quantity = quantity;
            _context.CartItems.Update(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveFromCartAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}

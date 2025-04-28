using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Authorization;

namespace EasyBeautyASP.Pages
{
    [Authorize(Roles = "Admin")]
    public class DeleteProductsModel : PageModel
    {


        private readonly ApplicationDbContext _db;

        public DeleteProductsModel(ApplicationDbContext db)
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

        public IActionResult OnPost(int id)
        {
            Products = _db.Products.Find(id);

            if (Products == null)
            {
                return NotFound();
            }

            _db.Products.Remove(Products);
            _db.SaveChanges();

            return RedirectToPage("./ShopAdmin");
        }
    }
}

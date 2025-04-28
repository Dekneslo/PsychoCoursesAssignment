using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Authorization;

namespace EasyBeautyASP.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditProductsModel : PageModel
    {

        public readonly ApplicationDbContext _db;

        [BindProperty]
        public Product Products { get; set; }
        public EditProductsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            Products = _db.Products.Find(id);

            if (Products == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {

            if (Products == null)
            {
                return NotFound();
            }

            _db.Products.Update(Products);
            _db.SaveChanges();

            return RedirectToPage("./Shop");

        }
    }
}

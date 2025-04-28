using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Authorization;

namespace EasyBeautyASP.Pages
{

    [Authorize(Roles = "Admin")]
    public class NewProductsModel : PageModel
    {

        public readonly ApplicationDbContext _db;

        [BindProperty]
        public Product Products { get; set; }


        public NewProductsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _db.Products.Add(Products);
                _db.SaveChanges();

                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {                
                Console.Error.WriteLine(ex.ToString());

                ModelState.AddModelError("", "An error occurred while adding the product.");

                return Page();
            }
        }
    }
}

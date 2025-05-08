using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace EasyBeautyASP.Pages
{
    [Authorize(Roles = "Admin")]
    public class NewProductsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;

        [BindProperty]
        public Product Products { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Пожалуйста, выберите изображение.")]
        public IFormFile? UploadedImage { get; set; }

        public NewProductsModel(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (UploadedImage == null || UploadedImage.Length == 0)
            {
                ModelState.AddModelError("UploadedImage", "Изображение обязательно.");
                return Page();
            }

            // Пропускаем валидацию ProductImage, так как оно будет установлено вручную
            ModelState.Remove("Products.ProductImage");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var fileName = Path.GetFileName(UploadedImage.FileName);
                var savePath = Path.Combine(_environment.WebRootPath, "assets", "media", "images", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                    UploadedImage.CopyTo(stream);

                Products.ProductImage = fileName;

                _db.Products.Add(Products);
                _db.SaveChanges();

                return RedirectToPage("ShopAdmin");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("❌ Ошибка: " + ex.Message);
                ModelState.AddModelError("", "Ошибка при добавлении товара.");
                return Page();
            }
        }


    }
}

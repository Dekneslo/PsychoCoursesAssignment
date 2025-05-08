using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EasyBeautyASP.Pages
{
    [Authorize(Roles = "Admin")]
    public class CreateBlogModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        [BindProperty]
        public Blog Blog { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Пожалуйста, выберите изображение.")]
        public IFormFile? UploadedImage { get; set; }

        public CreateBlogModel(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult OnPost()
        {
            if (UploadedImage == null || UploadedImage.Length == 0)
            {
                ModelState.AddModelError("UploadedImage", "Изображение обязательно.");
                return Page();
            }

            // Пропускаем валидацию поля Image, так как оно будет установлено вручную
            ModelState.Remove("Blog.Image");

            if (!ModelState.IsValid)
                return Page();

            try
            {
                var fileName = Path.GetFileName(UploadedImage.FileName);
                var blogImagesPath = Path.Combine(_env.WebRootPath, "assets", "media", "blogs");

                // ✅ Убедиться, что директория существует
                if (!Directory.Exists(blogImagesPath))
                    Directory.CreateDirectory(blogImagesPath);

                var savePath = Path.Combine(blogImagesPath, fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                    UploadedImage.CopyTo(stream);

                Blog.Image = fileName;
                Blog.DatePublished = DateTime.UtcNow;

                _db.Blogs.Add(Blog);
                _db.SaveChanges();

                return RedirectToPage("ShopAdmin");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("❌ Ошибка: " + ex.Message);
                ModelState.AddModelError("", "Ошибка при добавлении статьи.");
                return Page();
            }
        }
    }
}

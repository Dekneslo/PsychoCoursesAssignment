using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyBeautyASP.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditBlogModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public EditBlogModel(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [BindProperty] public Blog Blog { get; set; }
        [BindProperty] public IFormFile? UploadedImage { get; set; }

        public IActionResult OnGet(int id)
        {
            Blog = _db.Blogs.FirstOrDefault(b => b.Id == id);
            if (Blog == null) return NotFound();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var existingBlog = _db.Blogs.FirstOrDefault(b => b.Id == Blog.Id);
            if (existingBlog == null) return NotFound();

            existingBlog.Title = Blog.Title;
            existingBlog.Description = Blog.Description;
            existingBlog.Content = Blog.Content;

            if (UploadedImage != null)
            {
                var fileName = Path.GetFileName(UploadedImage.FileName);
                var path = Path.Combine(_env.WebRootPath, "assets", "media", "blogs");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var fullPath = Path.Combine(path, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                UploadedImage.CopyTo(stream);

                existingBlog.Image = fileName;
            }

            _db.SaveChanges();
            return RedirectToPage("ShopAdmin");
        }
    }
}

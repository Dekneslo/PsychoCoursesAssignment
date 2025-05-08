using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyBeautyASP.Pages
{
    public class BlogDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Blog Blog { get; set; }

        public BlogDetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            Blog = _db.Blogs.FirstOrDefault(b => b.Id == id);
            if (Blog == null) return NotFound();
            return Page();
        }
    }
}

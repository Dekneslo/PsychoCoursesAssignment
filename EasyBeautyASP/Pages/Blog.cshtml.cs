using EasyBeautyASP.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyBeautyASP.Pages
{
    public class BlogsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public BlogsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Blog> Blogs { get; set; }

        public void OnGet()
        {
            Blogs = _db.Blogs.OrderByDescending(b => b.DatePublished).ToList();
        }
    }
}

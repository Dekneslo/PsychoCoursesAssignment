using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EasyBeautyASP.Models;

namespace EasyBeautyASP.Pages
{
    public class MyCoursesModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyCoursesModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public List<Product> Courses { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Courses = await _db.UserCourses
                    .Include(uc => uc.Product)
                    .Where(uc => uc.UserId == user.Id)
                    .Select(uc => uc.Product)
                    .ToListAsync();
            }
        }
    }
}

using EasyBeautyASP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EasyBeautyASP.Models

{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }      

        public DbSet<Product> Products { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        
    }


    
}

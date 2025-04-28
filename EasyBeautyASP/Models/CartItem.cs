using Microsoft.AspNetCore.Identity;

namespace EasyBeautyASP.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}

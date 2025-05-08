namespace EasyBeautyASP.Models
{
    public class UserCourse
    {
        public int Id { get; set; }

        public string UserId { get; set; }  // внешний ключ на AspNetUsers
        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }  // внешний ключ на Product
        public Product Product { get; set; }

        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    }

}

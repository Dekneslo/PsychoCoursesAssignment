using System.ComponentModel.DataAnnotations;

namespace EasyBeautyASP.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Content { get; set; }

        public string? Image { get; set; }

        public DateTime DatePublished { get; set; } = DateTime.Now;
    }
}

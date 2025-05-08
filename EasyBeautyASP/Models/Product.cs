using System.ComponentModel.DataAnnotations;

namespace EasyBeautyASP.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Введите название товара")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Введите первое описание")]
        public string ProductFirstDescription { get; set; }

        [Required(ErrorMessage = "Введите второе описание")]
        public string ProductSecondDescription { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public int ProductPrice { get; set; }

        [Required(ErrorMessage = "Укажите ссылку для скачивания")]
        public string? ProductDownloadLink { get; set; }  // временно nullable


        [Display(Name = "Файл изображения")]
        public string? ProductImage { get; set; }  // <-- теперь nullable
    }
}

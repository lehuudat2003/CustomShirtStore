using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace CustomShirtStore.ViewModels
{
    public class DesignShirtViewModel
    {
        [Required]
        public long ProductId { get; set; }

        [Required]
        [Display(Name = "Tên thiết kế")]
        public string DesignName { get; set; } = null!;

        [Required]
        [Display(Name = "Tải ảnh thiết kế")]
        public IFormFile DesignImage { get; set; } = null!;
    }
}

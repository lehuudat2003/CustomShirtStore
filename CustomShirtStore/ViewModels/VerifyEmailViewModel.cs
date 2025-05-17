using System.ComponentModel.DataAnnotations;

namespace CustomShirtStore.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "bạn cần nhập email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }
    }
}

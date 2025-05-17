using System.ComponentModel.DataAnnotations;

namespace CustomShirtStore.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập Email.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Bạn cần nhập mật khẩu từ 8 - 40 ký tự")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp.")]
        [Required(ErrorMessage = "bạn cần xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmNewPassword { get; set; }
        public string Token { get; set; }
    }
}

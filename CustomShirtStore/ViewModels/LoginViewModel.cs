using System.ComponentModel.DataAnnotations;

namespace CustomShirtStore.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "bạn cần điền email!")]
        [EmailAddress(ErrorMessage ="bạn cần điền email hợp lệ!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "bạn cần điền mật khẩu!")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Ghi nhớ tài khoản")]
        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CustomShirtStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "bạn cần điền tên!.")]
        [Display(Name="Tên người dùng")]
        public string Name { get; set; }
        [Required(ErrorMessage = "bạn cần điền email!.")]
        [EmailAddress(ErrorMessage = "định dạng email không hợp lệ.")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "bạn cần nhập số điện thoại.")]
        [RegularExpression(@"^0[0-9]{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "bạn cần nhập mật khẩu!.")]
        [StringLength(100, ErrorMessage = "Mật khẩu cần dài ít nhất {2} ký tự .", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "bạn cần nhập mật khẩu xác nhận.")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Address { get; set; }
    }
}

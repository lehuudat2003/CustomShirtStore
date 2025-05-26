using CustomShirtStore.Models;
using System.ComponentModel.DataAnnotations;
namespace CustomShirtStore.ViewModels
{
    public class OrderInformationViewModel
    {
        public List<CartItem> CartItems { get; set; } = new();
        [Required(ErrorMessage = "Bạn cần nhập tên.")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
        [Display(Name = "Tên người nhận")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        [Display(Name = "Email người nhận")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại.")]
        [RegularExpression(@"^0[0-9]{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [Display(Name = "Số điện thoại người nhận")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập địa chỉ.")]
        [Display(Name= "Tỉnh/Thành Phố")]
        public string City { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập địa chỉ.")]
        [Display(Name = "Quận/Huyện")]
        public string District { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập địa chỉ.")]
        [Display(Name = "Phường/Xã")]
        public string Ward { get; set; }

        [Required(ErrorMessage = "Số nhà")]
        [Display(Name = "Số nhà/Ngõ")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn ít nhất một sản phẩm.")]
        [Display(Name= "Tổng số tiền")]
        public double TotalAmount { get; set; }
    }
}

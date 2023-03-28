using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "ID tài khoản không được để trống")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [RegularExpression("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)$", ErrorMessage = "Email không hợp lệ")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("UserPassword", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }

        public int RoleID { get; set; } = 2;
    }
}

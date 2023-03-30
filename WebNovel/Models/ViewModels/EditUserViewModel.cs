using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string UserID { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Hãy nhập mật khẩu cũ")]
        public string oldPassword { get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu mới")]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("newPassword", ErrorMessage = "Mật khẩu không khớp")]
        public string confirmPassword { get; set; }


        [Required(ErrorMessage = "Email không được để trống")]
        [RegularExpression("^[A-Za-z0-9+_.-]+@(.+)$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required]
        public int RoleID { get; set; }
    }
}

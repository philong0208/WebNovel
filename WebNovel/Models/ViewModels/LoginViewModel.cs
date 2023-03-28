using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models
{
    public class LoginViewModel
    {
        [DisplayName("ID Tài khoản:")]
        [Required(ErrorMessage = "ID Tài khoản không được để trống")]
        public string userID { get; set; }
        [DisplayName("Mật khẩu:")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string password { get; set; }
        [DisplayName("Duy trì đăng nhập")]
        public bool keepLoggedIn { get; set; }
    }
}

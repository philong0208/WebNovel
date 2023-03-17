using Microsoft.Build.Framework;
using System.ComponentModel;

namespace WebNovel.Models
{
    public class LoginViewModel
    {
        [DisplayName("Tên Đăng nhập")]
        [Required]
        public String userID { get; set; }
        [DisplayName("Mật khẩu")]
        [Required]
        public String password { get; set; }
        [DisplayName("Duy trì đăng nhập")]
        public bool keepLoggedIn { get; set; }
    }
}

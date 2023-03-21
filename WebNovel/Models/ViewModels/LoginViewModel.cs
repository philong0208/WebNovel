using Microsoft.Build.Framework;
using System.ComponentModel;

namespace WebNovel.Models
{
    public class LoginViewModel
    {
        [DisplayName("Tên Đăng nhập")]
        [Required]
        public string userID { get; set; }
        [DisplayName("Mật khẩu")]
        [Required]
        public string password { get; set; }
        [DisplayName("Duy trì đăng nhập")]
        public bool keepLoggedIn { get; set; }
    }
}

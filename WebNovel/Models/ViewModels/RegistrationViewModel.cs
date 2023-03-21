using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models
{
    public class RegistrationViewModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        [Compare("UserPassword")]
        public string ConfirmPassword { get; set; }
        public int RoleID { get; set; }
    }
}

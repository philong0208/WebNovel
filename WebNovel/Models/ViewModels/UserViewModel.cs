using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebNovel.Models.ViewModels
{
    public class UserViewModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Novel> Novels { get; set; }
    }
}

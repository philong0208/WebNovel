using Microsoft.AspNetCore.Identity;

namespace WebNovel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string name { get; set; }
        public string ?  profilePicture { get; set; }
    }
}

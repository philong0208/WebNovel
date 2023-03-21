using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models.ViewModels
{
    public class AuthorViewModel
    {
        [Key]
        public int AuthorID { get; set; }
        [Required]
        [DisplayName("Tên tác giả")]
        public string AuthorName { get; set; }

        public virtual ICollection<Novel> Novels { get; set; }
    }
}

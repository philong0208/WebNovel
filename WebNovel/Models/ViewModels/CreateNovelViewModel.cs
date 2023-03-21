using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models.ViewModels
{
    public class CreateNovelViewModel
    {
        [Required]
        public String NovelTitle { get; set; }
        [Required]
        public String NovelDescription { get; set; }
        [Required]
        public String NovelCover { get; set; }

        [Required]
        public string UserID { get; set; }
        [Required]
        public int AuthorID { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models.ViewModels
{
    public class EditNovelViewModel
    {
        [Key]
        public int NovelID { get; set; }
        [Required]
        public String NovelTitle { get; set; }
        [Required]
        public String NovelDescription { get; set; }
        [Required]
        public String NovelCover { get; set; }

        [Required]
        public int AuthorID { get; set; }

        public List<int> GenreID { get; set; }
    }
}

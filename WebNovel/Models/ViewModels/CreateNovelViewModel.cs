using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models.ViewModels
{
    public class CreateNovelViewModel
    {
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public String NovelTitle { get; set; }
        [Required(ErrorMessage = "Phải mô tả nội dung sơ lược")]
        public String NovelDescription { get; set; }
        
        [Required(ErrorMessage = "Hãy chọn tác giả")]
        public int AuthorID { get; set; }
        [Required(ErrorMessage = "Hãy chọn ít nhất 1 thể loại")]
        public List<int> GenreId { get; set; }
    }
}

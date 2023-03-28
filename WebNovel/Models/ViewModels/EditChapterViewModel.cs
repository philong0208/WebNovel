using System.ComponentModel;

namespace WebNovel.Models.ViewModels
{
    public class EditChapterViewModel
    {
        public int ChapterId { get; set; }

        [DisplayName("Tiêu đề chương:")]
        public string ChapterTitle { get; set; } = null!;

        [DisplayName("Nội dung:")]
        public string ChapterContent { get; set; } = null!;
    }
}

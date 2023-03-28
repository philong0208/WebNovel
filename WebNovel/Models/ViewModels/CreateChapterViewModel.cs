using System.ComponentModel;

namespace WebNovel.Models.ViewModels
{
    public class CreateChapterViewModel
    {
        [DisplayName("Tiêu đề chương:")]
        public string ChapterTitle { get; set; } = null!;

        [DisplayName("Nội dung:")]
        public string ChapterContent { get; set; } = null!;

        [DisplayName("Ngày đăng:")]
        public DateTime ChapterDatePost { get; set; } = DateTime.Now;

        public int NovelId { get; set; }
    }
}

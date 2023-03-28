using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebNovel.Models;

public partial class Chapter
{
    public int ChapterId { get; set; }

    [DisplayName("Tiêu đề chương:")]
    public string ChapterTitle { get; set; } = null!;

    [DisplayName("Nội dung:")]
    public string ChapterContent { get; set; } = null!;

    //[DisplayName("Ngày đăng:")]
    public DateTime ChapterDatePost { get; set; }

    public int NovelId { get; set; }

    public virtual Novel Novel { get; set; } = null!;
}

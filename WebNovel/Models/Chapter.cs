using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class Chapter
{
    public int ChapterId { get; set; }

    public string ChapterTitle { get; set; } = null!;

    public string ChapterContent { get; set; } = null!;

    public DateTime ChapterDatePost { get; set; }

    public int NovelId { get; set; }

    public virtual Novel Novel { get; set; } = null!;
}

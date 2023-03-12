using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class Chapter
{
    public int ChapterId { get; set; }

    public int NovelId { get; set; }

    public string Title { get; set; } = null!;

    public string ChapterContent { get; set; } = null!;

    public DateTime DatePost { get; set; }

    public virtual Novel Novel { get; set; } = null!;
}

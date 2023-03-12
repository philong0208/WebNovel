using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class UserComment
{
    public int CommentId { get; set; }

    public string UserId { get; set; } = null!;

    public int NovelId { get; set; }

    public string CommentContent { get; set; } = null!;

    public DateTime DatePost { get; set; }

    public DateTime DateEdit { get; set; }

    public virtual Novel Novel { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

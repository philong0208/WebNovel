using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class UserRating
{
    public int RatingId { get; set; }

    public string UserId { get; set; } = null!;

    public int NovelId { get; set; }

    public int Score { get; set; }

    public DateTime DatePost { get; set; }

    public DateTime DateEdit { get; set; }

    public virtual Novel Novel { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class UserRating
{
    public int RatingId { get; set; }

    public int RatingScore { get; set; }

    public DateTime RatingDatePost { get; set; }

    public DateTime? RatingDateEdit { get; set; }

    public string UserId { get; set; } = null!;

    public int NovelId { get; set; }

    public virtual Novel Novel { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

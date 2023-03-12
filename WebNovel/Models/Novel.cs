using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class Novel
{
    public int NovelId { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    public string Description { get; set; } = null!;

    public byte[] Cover { get; set; } = null!;

    public DateTime DatePost { get; set; }

    public int? View { get; set; }

    public string UserId { get; set; } = null!;

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Chapter> Chapters { get; } = new List<Chapter>();

    public virtual NovelGenre NovelNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserComment> UserComments { get; } = new List<UserComment>();

    public virtual ICollection<UserRating> UserRatings { get; } = new List<UserRating>();
}

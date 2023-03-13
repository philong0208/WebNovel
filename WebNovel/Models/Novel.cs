using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class Novel
{
    public int NovelId { get; set; }

    public string NovelTitle { get; set; } = null!;

    public string NovelDescription { get; set; } = null!;

    public string NovelCover { get; set; } = null!;

    public DateTime NovelDatePost { get; set; }

    public int NovelView { get; set; }

    public int AuthorId { get; set; }

    public string UserId { get; set; } = null!;

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Chapter> Chapters { get; } = new List<Chapter>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserComment> UserComments { get; } = new List<UserComment>();

    public virtual ICollection<UserRating> UserRatings { get; } = new List<UserRating>();

    public virtual ICollection<Genre> Genres { get; } = new List<Genre>();
}

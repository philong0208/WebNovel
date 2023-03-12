using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class NovelGenre
{
    public int NovelId { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Novel? Novel { get; set; }
}

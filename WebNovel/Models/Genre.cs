using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public string GenreDescription { get; set; } = null!;

    public virtual ICollection<NovelGenre> NovelGenres { get; } = new List<NovelGenre>();
}

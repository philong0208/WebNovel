using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebNovel.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    [DisplayName("Tên thể loại")]
    public string GenreName { get; set; } = null!;

    [DisplayName("Mô tả")]
    public string GenreDescription { get; set; } = null!;

    public virtual ICollection<Novel> Novels { get; } = new List<Novel>();
}

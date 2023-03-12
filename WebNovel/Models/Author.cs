using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = null!;

    public virtual ICollection<Novel> Novels { get; } = new List<Novel>();
}

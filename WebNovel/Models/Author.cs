using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebNovel.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    [DisplayName("Tên tác giả")]    
    public string AuthorName { get; set; } = null!;

    public virtual ICollection<Novel> Novels { get; } = new List<Novel>();
}

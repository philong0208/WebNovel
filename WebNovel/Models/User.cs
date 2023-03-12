using System;
using System.Collections.Generic;

namespace WebNovel.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Novel> Novels { get; } = new List<Novel>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserComment> UserComments { get; } = new List<UserComment>();

    public virtual ICollection<UserRating> UserRatings { get; } = new List<UserRating>();
}

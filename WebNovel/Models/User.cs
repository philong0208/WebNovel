using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models;

public partial class User
{
    [DisplayName("Tên tài khoản")]
    public string UserId { get; set; } = null!;

    [DisplayName("Họ và tên")]
    public string UserName { get; set; } = null!;

    [DisplayName("Mật khẩu")]
    public string UserPassword { get; set; } = null!;

    [DisplayName("Email")]
    public string UserEmail { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Novel> Novels { get; } = new List<Novel>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserComment> UserComments { get; } = new List<UserComment>();

    public virtual ICollection<UserRating> UserRatings { get; } = new List<UserRating>();
}

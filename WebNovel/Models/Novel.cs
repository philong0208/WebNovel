using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebNovel.Models;

public partial class Novel
{
    [Key]
    public int NovelId { get; set; }

    [DisplayName("Tên")]
    public string NovelTitle { get; set; } = null!;

    [DisplayName("Mô tả")]
    public string NovelDescription { get; set; } = null!;

    public string NovelCover { get; set; } = null!;

    [DisplayName("Ngày đăng")]
    [DataType(DataType.Date)]
    public DateTime NovelDatePost { get; set; }

    [DisplayName("Lượt xem")]
    public int NovelView { get; set; } = 1;

    [DisplayName("Tác giả")]
    public int AuthorId { get; set; }

    [DisplayName("Người đăng")]
    public string UserId { get; set; } = null!;

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Chapter> Chapters { get; } = new List<Chapter>();

    public virtual User User { get; set; } = null!;

    [DisplayName("Bình luận")]
    public virtual ICollection<UserComment> UserComments { get; } = new List<UserComment>();

    [DisplayName("Đánh giá")]
    public virtual ICollection<UserRating> UserRatings { get; } = new List<UserRating>();

    [DisplayName("Thể loại")]
    public virtual ICollection<Genre> Genres { get; } = new List<Genre>();
}

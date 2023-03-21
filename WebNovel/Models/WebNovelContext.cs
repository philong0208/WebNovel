using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebNovel.Models.ViewModels;

namespace WebNovel.Models;

public partial class WebNovelContext : DbContext
{
    public WebNovelContext()
    {
    }

    public WebNovelContext(DbContextOptions<WebNovelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Novel> Novels { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserComment> UserComments { get; set; }

    public virtual DbSet<UserRating> UserRatings { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=PHILONG;Initial Catalog=WEB_NOVEL;User ID=sa;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("AUTHOR");

            entity.Property(e => e.AuthorId).HasColumnName("Author_ID");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .HasColumnName("Author_Name");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.ToTable("CHAPTER");

            entity.Property(e => e.ChapterId).HasColumnName("Chapter_ID");
            entity.Property(e => e.ChapterContent)
                .HasColumnType("ntext")
                .HasColumnName("Chapter_Content");
            entity.Property(e => e.ChapterDatePost)
                .HasColumnType("date")
                .HasColumnName("Chapter_DatePost");
            entity.Property(e => e.ChapterTitle)
                .HasMaxLength(500)
                .HasColumnName("Chapter_Title");
            entity.Property(e => e.NovelId).HasColumnName("Novel_ID");

            entity.HasOne(d => d.Novel).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.NovelId)
                .HasConstraintName("FK_CHAPTER_NOVEL");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("GENRE");

            entity.Property(e => e.GenreId).HasColumnName("Genre_ID");
            entity.Property(e => e.GenreDescription)
                .HasMaxLength(100)
                .HasColumnName("Genre_Description");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .HasColumnName("Genre_Name");
        });

        modelBuilder.Entity<Novel>(entity =>
        {
            entity.ToTable("NOVEL");

            entity.Property(e => e.NovelId).HasColumnName("Novel_ID");
            entity.Property(e => e.AuthorId).HasColumnName("Author_ID");
            entity.Property(e => e.NovelCover)
                .HasMaxLength(1000)
                .HasColumnName("Novel_Cover");
            entity.Property(e => e.NovelDatePost)
                .HasColumnType("date")
                .HasColumnName("Novel_DatePost");
            entity.Property(e => e.NovelDescription)
                .HasMaxLength(500)
                .HasColumnName("Novel_Description");
            entity.Property(e => e.NovelTitle)
                .HasMaxLength(100)
                .HasColumnName("Novel_Title");
            entity.Property(e => e.NovelView).HasColumnName("Novel_View");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.Author).WithMany(p => p.Novels)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_NOVEL_AUTHOR");

            entity.HasOne(d => d.User).WithMany(p => p.Novels)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_NOVEL_USER");

            entity.HasMany(d => d.Genres).WithMany(p => p.Novels)
                .UsingEntity<Dictionary<string, object>>(
                    "NovelGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_NOVEL_GENRE_GENRE"),
                    l => l.HasOne<Novel>().WithMany()
                        .HasForeignKey("NovelId")
                        .HasConstraintName("FK_NOVEL_GENRE_NOVEL"),
                    j =>
                    {
                        j.HasKey("NovelId", "GenreId");
                        j.ToTable("NOVEL_GENRE");
                        j.IndexerProperty<int>("NovelId").HasColumnName("Novel_ID");
                        j.IndexerProperty<int>("GenreId").HasColumnName("Genre_ID");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("ROLE");

            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USER");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");
            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .HasColumnName("User_Email");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_Name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(20)
                .HasColumnName("User_Password");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_USER_ROLE");
        });

        modelBuilder.Entity<UserComment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("USER_COMMENT");

            entity.Property(e => e.CommentId).HasColumnName("Comment_ID");
            entity.Property(e => e.CommentContent)
                .HasColumnType("ntext")
                .HasColumnName("Comment_Content");
            entity.Property(e => e.CommentDateEdit)
                .HasColumnType("date")
                .HasColumnName("Comment_DateEdit");
            entity.Property(e => e.CommentDatePost)
                .HasColumnType("date")
                .HasColumnName("Comment_DatePost");
            entity.Property(e => e.NovelId).HasColumnName("Novel_ID");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.Novel).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_COMMENT_NOVEL");

            entity.HasOne(d => d.User).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USER_COMMENT_USER");
        });

        modelBuilder.Entity<UserRating>(entity =>
        {
            entity.HasKey(e => e.RatingId);

            entity.ToTable("USER_RATING");

            entity.Property(e => e.RatingId).HasColumnName("Rating_ID");
            entity.Property(e => e.NovelId).HasColumnName("Novel_ID");
            entity.Property(e => e.RatingDateEdit)
                .HasColumnType("date")
                .HasColumnName("Rating_DateEdit");
            entity.Property(e => e.RatingDatePost)
                .HasColumnType("date")
                .HasColumnName("Rating_DatePost");
            entity.Property(e => e.RatingScore).HasColumnName("Rating_Score");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.Novel).WithMany(p => p.UserRatings)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_RATING_NOVEL");

            entity.HasOne(d => d.User).WithMany(p => p.UserRatings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USER_RATING_USER");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

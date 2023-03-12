using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebNovel.Models;

public partial class NovelReaderContext : DbContext
{
    public NovelReaderContext()
    {
    }

    public NovelReaderContext(DbContextOptions<NovelReaderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Novel> Novels { get; set; }

    public virtual DbSet<NovelGenre> NovelGenres { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserComment> UserComments { get; set; }

    public virtual DbSet<UserRating> UserRatings { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=PHILONG;Initial Catalog=NOVEL_READER;User ID=sa;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedNever()
                .HasColumnName("Author_ID");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .HasColumnName("Author_Name");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.ToTable("Chapter");

            entity.Property(e => e.ChapterId)
                .ValueGeneratedNever()
                .HasColumnName("Chapter_ID");
            entity.Property(e => e.ChapterContent)
                .HasColumnType("text")
                .HasColumnName("Chapter_Content");
            entity.Property(e => e.DatePost)
                .HasColumnType("date")
                .HasColumnName("Date_Post");
            entity.Property(e => e.NovelId).HasColumnName("Novel_ID");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Novel).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.NovelId)
                .HasConstraintName("FK_Chapter_Novel");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.GenreId)
                .ValueGeneratedNever()
                .HasColumnName("Genre_ID");
            entity.Property(e => e.GenreDescription)
                .HasMaxLength(100)
                .HasColumnName("Genre_Description");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .HasColumnName("Genre_Name");
        });

        modelBuilder.Entity<Novel>(entity =>
        {
            entity.ToTable("Novel");

            entity.Property(e => e.NovelId)
                .ValueGeneratedNever()
                .HasColumnName("Novel_ID");
            entity.Property(e => e.AuthorId).HasColumnName("Author_ID");
            entity.Property(e => e.Cover).HasColumnType("image");
            entity.Property(e => e.DatePost)
                .HasColumnType("date")
                .HasColumnName("Date_Post");
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.Author).WithMany(p => p.Novels)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Novel_Author");

            entity.HasOne(d => d.NovelNavigation).WithOne(p => p.Novel)
                .HasForeignKey<Novel>(d => d.NovelId)
                .HasConstraintName("FK_Novel_Novel_Genre");

            entity.HasOne(d => d.User).WithMany(p => p.Novels)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Novel_User");
        });

        modelBuilder.Entity<NovelGenre>(entity =>
        {
            entity.HasKey(e => e.NovelId);

            entity.ToTable("Novel_Genre");

            entity.Property(e => e.NovelId)
                .ValueGeneratedNever()
                .HasColumnName("Novel_ID");
            entity.Property(e => e.GenreId).HasColumnName("Genre_ID");

            entity.HasOne(d => d.Genre).WithMany(p => p.NovelGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Novel_Genre_Genre");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("ROLE");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("Role_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_ROLE");
        });

        modelBuilder.Entity<UserComment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("User_Comment");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("Comment_ID");
            entity.Property(e => e.CommentContent)
                .HasMaxLength(500)
                .HasColumnName("Comment_Content");
            entity.Property(e => e.DateEdit)
                .HasColumnType("date")
                .HasColumnName("Date_Edit");
            entity.Property(e => e.DatePost)
                .HasColumnType("date")
                .HasColumnName("Date_Post");
            entity.Property(e => e.NovelId).HasColumnName("Novel_ID");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.Novel).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Comment_Novel");

            entity.HasOne(d => d.User).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Comment_User");
        });

        modelBuilder.Entity<UserRating>(entity =>
        {
            entity.HasKey(e => e.RatingId);

            entity.ToTable("User_Rating");

            entity.Property(e => e.RatingId)
                .ValueGeneratedNever()
                .HasColumnName("Rating_ID");
            entity.Property(e => e.DateEdit)
                .HasColumnType("date")
                .HasColumnName("Date_Edit");
            entity.Property(e => e.DatePost)
                .HasColumnType("date")
                .HasColumnName("Date_Post");
            entity.Property(e => e.NovelId).HasColumnName("Novel_ID");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.Novel).WithMany(p => p.UserRatings)
                .HasForeignKey(d => d.NovelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Rating_Novel");

            entity.HasOne(d => d.User).WithMany(p => p.UserRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Rating_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

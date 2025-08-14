using EfCore9Demo.App.models;
using Microsoft.EntityFrameworkCore;

namespace EfCore9Demo.App
{
    public class AppDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogDetail> BlogDetails { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=.;Database=EfCore9DemoDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-many: Blog → Post
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many: Post → Comment
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one: Blog ↔ BlogDetail
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.BlogDetail)
                .WithOne(d => d.Blog)
                .HasForeignKey<BlogDetail>(d => d.BlogId);

            // Many-to-many: Post ↔ Tag
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity(j => j.ToTable("PostTags"));

            // Fixed datetime for seeding
            var seedDate = new DateTime(2025, 8, 15, 14, 0, 0);

            // Seed Blog
            modelBuilder.Entity<Blog>().HasData(new Blog
            {
                BlogId = 1,
                Url = "https://example.com",
                Title = "Tech Insights",
                OwnerName = "John Doe",
                OwnerEmail = "john@example.com",
                CreatedAt = seedDate
            });

            // Seed BlogDetail
            modelBuilder.Entity<BlogDetail>().HasData(new BlogDetail
            {
                BlogId = 1,
                Description = "A blog about cutting-edge tech.",
                LastUpdated = seedDate
            });

            // Seed Tags
            modelBuilder.Entity<Tag>().HasData(
                new Tag { TagId = 1, Name = "EFCore" },
                new Tag { TagId = 2, Name = "CSharp" }
            );

            // Seed Posts
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    PostId = 1,
                    BlogId = 1,
                    Title = "Hello EF Core 9",
                    Content = "This is a post introducing EF Core 9.",
                    PublishedAt = seedDate,
                    Rating = 4.5m,
                    ReadTimeMinutes = 5.2,
                    IsPublished = true
                },
                new Post
                {
                    PostId = 2,
                    BlogId = 1,
                    Title = "Advanced EF Core 9 Queries",
                    Content = "A deep dive into EF Core 9's new LINQ capabilities.",
                    PublishedAt = seedDate,
                    Rating = 5.0m,
                    ReadTimeMinutes = 8.7,
                    IsPublished = true
                }
            );

            // Seed Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    CommentId = 1,
                    PostId = 1,
                    AuthorName = "Alice",
                    AuthorEmail = "alice@example.com",
                    Text = "Nice article!",
                    Status = CommentStatus.Approved,
                    CreatedAt = seedDate
                },
                new Comment
                {
                    CommentId = 2,
                    PostId = 1,
                    AuthorName = "Bob",
                    AuthorEmail = "bob@example.com",
                    Text = "Very informative, thanks!",
                    Status = CommentStatus.Approved,
                    CreatedAt = seedDate
                },
                new Comment
                {
                    CommentId = 3,
                    PostId = 2,
                    AuthorName = "Charlie",
                    AuthorEmail = "charlie@example.com",
                    Text = "Looking forward to trying these out!",
                    Status = CommentStatus.Pending,
                    CreatedAt = seedDate
                }
            );

            // Seed Many-to-Many links (Post ↔ Tag)
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTags",
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagsTagId"),
                    j => j.HasOne<Post>().WithMany().HasForeignKey("PostsPostId"),
                    j =>
                    {
                        j.HasKey("PostsPostId", "TagsTagId"); // <-- define composite PK
                        j.HasData(
                            new { PostsPostId = 1, TagsTagId = 1 },
                            new { PostsPostId = 1, TagsTagId = 2 },
                            new { PostsPostId = 2, TagsTagId = 1 }
                        );
                    });

        }
    }
}

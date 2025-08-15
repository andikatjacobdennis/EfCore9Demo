using EfCore9Demo.App;
using EfCore9Demo.App.models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        Console.WriteLine("EF Core 9 Demo Application");
        RunDemo();
    }

    private static void RunDemo()
    {
        using (var context = new AppDbContext())
        {
            // Ensure database exists (migrations + seeding already applied)
            context.Database.EnsureCreated();

            // Insert extra blog, posts, comments, and tags at runtime
            var extraTag = new Tag { Name = "Database" };
            var extraBlog = new Blog
            {
                Url = "https://datainsights.com",
                Title = "Data Insights",
                OwnerName = "Jane Smith",
                OwnerEmail = "jane@datainsights.com",
                CreatedAt = DateTime.UtcNow,
                BlogDetail = new BlogDetail
                {
                    Description = "Exploring data technologies and trends.",
                    LastUpdated = DateTime.UtcNow
                },
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "Understanding EF Core Seeding",
                        Content = "A guide to seeding data in EF Core with relationships.",
                        PublishedAt = DateTime.UtcNow,
                        Rating = 4.8m,
                        ReadTimeMinutes = 6.0,
                        IsPublished = true,
                        Tags = new List<Tag> { extraTag },
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                AuthorName = "Eve",
                                AuthorEmail = "eve@example.com",
                                Text = "Very helpful, thanks!",
                                Status = CommentStatus.Approved,
                                CreatedAt = DateTime.UtcNow
                            }
                        }
                    }
                }
            };

            context.Blogs.Add(extraBlog);
            context.SaveChanges();

            // Query all blogs with eager loading
            var blogs = context.Blogs
                .Include(b => b.BlogDetail)
                .Include(b => b.Posts)
                    .ThenInclude(p => p.Comments)
                .Include(b => b.Posts)
                    .ThenInclude(p => p.Tags)
                .ToList();

            // Display results
            foreach (var b in blogs)
            {
                Console.WriteLine($"\nBlog: {b.Title} ({b.Url})");
                Console.WriteLine($"Owner: {b.OwnerName} ({b.OwnerEmail})");
                Console.WriteLine($"Created: {b.CreatedAt}");

                if (b.BlogDetail != null)
                {
                    Console.WriteLine($"  Blog Detail: {b.BlogDetail.Description}");
                    Console.WriteLine($"  Last Updated: {b.BlogDetail.LastUpdated}");
                }

                foreach (var p in b.Posts)
                {
                    Console.WriteLine($"\n  Post: {p.Title}");
                    Console.WriteLine($"    Published: {p.PublishedAt}");
                    Console.WriteLine($"    Rating: {p.Rating}");
                    Console.WriteLine($"    Read Time: {p.ReadTimeMinutes} minutes");
                    Console.WriteLine($"    Published Status: {p.IsPublished}");

                    if (p.Tags.Any())
                    {
                        Console.WriteLine($"    Tags: {string.Join(", ", p.Tags.Select(t => t.Name))}");
                    }

                    foreach (var c in p.Comments)
                    {
                        Console.WriteLine($"      Comment by {c.AuthorName} ({c.AuthorEmail}): {c.Text} [Status: {c.Status}]");
                    }
                }

                // Demo of raw SQL query
                var posts = context.Posts.FromSql($"SELECT * FROM dbo.Posts").ToList();
                Console.WriteLine("\nRaw SQL Query Results:");
                Console.WriteLine($"Total Posts: {posts.Count}");
                foreach (var post in posts)
                {
                    Console.WriteLine($"\n  Raw SQL Post: {post.Title}");
                    Console.WriteLine($"    Content: {post.Content}");
                    Console.WriteLine($"    Published At: {post.PublishedAt}");
                    Console.WriteLine($"    Rating: {post.Rating}");
                    Console.WriteLine($"    Read Time: {post.ReadTimeMinutes} minutes");
                    Console.WriteLine($"    Is Published: {post.IsPublished}");
                }
            }
        }
    }
}

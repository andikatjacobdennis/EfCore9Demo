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
            // Clear database for demo purposes
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Create sample blog with posts and comments
            var blog = new Blog
            {
                Url = "https://example.com",
                Title = "Tech Insights",
                OwnerName = "John Doe",
                OwnerEmail = "john@example.com",
                CreatedAt = DateTime.UtcNow,
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "Hello EF Core 9",
                        Content = "This is a post introducing EF Core 9.",
                        PublishedAt = DateTime.UtcNow,
                        Rating = 4.5m,
                        ReadTimeMinutes = 5.2,
                        IsPublished = true,
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                AuthorName = "Alice",
                                AuthorEmail = "alice@example.com",
                                Text = "Nice article!",
                                Status = CommentStatus.Approved
                            },
                            new Comment
                            {
                                AuthorName = "Bob",
                                AuthorEmail = "bob@example.com",
                                Text = "Very informative, thanks!",
                                Status = CommentStatus.Approved
                            }
                        }
                    },
                    new Post
                    {
                        Title = "Advanced EF Core 9 Queries",
                        Content = "A deep dive into EF Core 9's new LINQ capabilities.",
                        PublishedAt = DateTime.UtcNow,
                        Rating = 5.0m,
                        ReadTimeMinutes = 8.7,
                        IsPublished = true,
                        Comments = new List<Comment>
                        {
                            new Comment
                            {
                                AuthorName = "Charlie",
                                AuthorEmail = "charlie@example.com",
                                Text = "Looking forward to trying these out!",
                                Status = CommentStatus.Pending
                            }
                        }
                    }
                }
            };

            context.Blogs.Add(blog);
            context.SaveChanges();

            // Query with eager loading
            var blogs = context.Blogs
                .Include(b => b.Posts)
                .ThenInclude(p => p.Comments)
                .ToList();

            // Display results
            foreach (var b in blogs)
            {
                Console.WriteLine($"\nBlog: {b.Title} ({b.Url})");
                Console.WriteLine($"Owner: {b.OwnerName} ({b.OwnerEmail})");
                Console.WriteLine($"Created: {b.CreatedAt}");

                foreach (var p in b.Posts)
                {
                    Console.WriteLine($"\n  Post: {p.Title}");
                    Console.WriteLine($"    Published: {p.PublishedAt}");
                    Console.WriteLine($"    Rating: {p.Rating}");
                    Console.WriteLine($"    Read Time: {p.ReadTimeMinutes} minutes");
                    Console.WriteLine($"    Published Status: {p.IsPublished}");

                    foreach (var c in p.Comments)
                    {
                        Console.WriteLine($"      Comment by {c.AuthorName} ({c.AuthorEmail}): {c.Text} [Status: {c.Status}]");
                    }
                }
            }
        }
    }
}

using EfCore9Demo.App;
using EfCore9Demo.App.models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("EF Core 9 Demo Application");
        RunDemo();
    }
    private static void RunDemo()
    {
        using (var context = new AppDbContext())
        {
            var blog = new Blog
            {
                Url = "https://example.com",
                Posts = new List<Post>
        {
            new Post
            {
                Title = "Hello EF Core 9",
                Content = "This is a post.",
                Comments = new List<Comment>
                {
                    new Comment { AuthorName = "Alice", Text = "Nice article!" }
                }
            }
        }
            };

            context.Blogs.Add(blog);
            context.SaveChanges();

            var blogs = context.Blogs
                .Include(b => b.Posts)
                .ThenInclude(p => p.Comments)
                .ToList();

            foreach (var b in blogs)
            {
                Console.WriteLine($"Blog: {b.Url}");
                foreach (var p in b.Posts)
                {
                    Console.WriteLine($"  Post: {p.Title}");
                    foreach (var c in p.Comments)
                    {
                        Console.WriteLine($"    Comment by {c.AuthorName}: {c.Text}");
                    }
                }
            }
        }
    }
}
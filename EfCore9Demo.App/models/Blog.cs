namespace EfCore9Demo.App.models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; set; } = new();
    }
}

using System.ComponentModel.DataAnnotations;

namespace EfCore9Demo.App.models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Url { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(100)]
        public string OwnerName { get; set; }

        [EmailAddress]
        public string OwnerEmail { get; set; }

        public BlogDetail BlogDetail { get; set; } // One-to-one navigation

        public List<Post> Posts { get; set; } = new();
    }
}
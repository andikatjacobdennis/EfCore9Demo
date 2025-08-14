using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCore9Demo.App.models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(5, 2)")]
        [Range(0, 5)]
        public decimal Rating { get; set; } = 0.0m;

        public bool IsPublished { get; set; } = false;

        [Range(0, double.MaxValue)]
        public double ReadTimeMinutes { get; set; } = 0.0;

        // Foreign key & navigation
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public List<Tag> Tags { get; set; } = new(); // One-to-one navigation

        public List<Comment> Comments { get; set; } = new();


    }
}
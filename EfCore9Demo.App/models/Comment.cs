using System.ComponentModel.DataAnnotations;

namespace EfCore9Demo.App.models
{
    public enum CommentStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [MaxLength(80)]
        public string AuthorName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [DataType(DataType.EmailAddress)]
        public string AuthorEmail { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public CommentStatus Status { get; set; } = CommentStatus.Pending;

        // Foreign key & navigation
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
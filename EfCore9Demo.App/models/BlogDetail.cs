using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EfCore9Demo.App.models
{
    public class BlogDetail
    {
        [Key]
        public int BlogId { get; set; } // Primary key also FK to Blog

        [MaxLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public Blog Blog { get; set; }
    }
}

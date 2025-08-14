using System.ComponentModel.DataAnnotations;


namespace EfCore9Demo.App.models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}

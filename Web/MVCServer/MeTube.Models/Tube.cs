namespace MeTube.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tube
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required]
        public string YoutubeId { get; set; }

        [Range(0, int.MaxValue)]
        public int Views { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}

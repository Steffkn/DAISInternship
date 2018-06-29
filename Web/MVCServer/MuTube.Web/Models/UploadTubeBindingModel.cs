namespace MuTube.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UploadTubeBindingModel
    {
        [Required]
        [MinLength(10)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string YouTubeLink { get; set; }
    }
}

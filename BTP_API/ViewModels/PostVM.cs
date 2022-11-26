using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class PostVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Image { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class UserVM
    {
        [Required]
        public string Fullname { get; set; }
        public int? Age { get; set; }
        [Required]
        public string AddressMain { get; set; }
        public string Avatar { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class UserVM
    {
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string AddressMain { get; set; }
        public string Avatar { get; set; }
    }
}

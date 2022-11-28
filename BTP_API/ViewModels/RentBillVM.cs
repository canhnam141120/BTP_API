using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class RentBillVM
    {   
        [Required]
        public bool IsPaid { get; set; }
        
        public DateTime? PaidDate { get; set; }
        
        public string Payment { get; set; }
    }
}

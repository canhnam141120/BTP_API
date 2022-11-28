using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class ExchangeBillVM
    {  
        [Required]
        public bool IsPaid { get; set; }
        
        public DateTime? PaidDate { get; set; }
        
        public string Payments { get; set; }
    }
}

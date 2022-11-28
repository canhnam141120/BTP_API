using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class ExchangeVM
    {
        public string StorageStatus1 { get; set; }
        
        public string StorageStatus2 { get; set; }

        public DateOnly? SendDate { get; set; }

        public DateOnly? ReceiveDate { get; set; }

        public DateOnly? RecallDate { get; set; }

        public DateOnly? RefundDate { get; set; }

        [Required]
        public string Status { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class ExchangeVM
    {
        public string StorageStatus1 { get; set; }
        
        public string StorageStatus2 { get; set; }

        public DateOnly? SendDate1 { get; set; }

        public DateOnly? ReceiveDate1 { get; set; }

        public DateOnly? RecallDate1 { get; set; }

        public DateOnly? RefundDate1 { get; set; }
        public DateOnly? SendDate2 { get; set; }

        public DateOnly? ReceiveDate2 { get; set; }

        public DateOnly? RecallDate2 { get; set; }

        public DateOnly? RefundDate2 { get; set; }
    }
}

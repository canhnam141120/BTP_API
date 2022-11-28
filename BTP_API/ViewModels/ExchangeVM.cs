using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class ExchangeVM
    {
        public string StorageStatus1 { get; set; }
        
        public string StorageStatus2 { get; set; }

        public string SendDate1 { get; set; }

        public string ReceiveDate1 { get; set; }

        public string RecallDate1 { get; set; }

        public string RefundDate1 { get; set; }
        public string SendDate2 { get; set; }

        public string ReceiveDate2 { get; set; }

        public string RecallDate2 { get; set; }

        public string RefundDate2 { get; set; }
    }
}

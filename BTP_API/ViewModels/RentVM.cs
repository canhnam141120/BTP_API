using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class RentVM
    {
        public string StorageStatus{ get; set; }
        
        public string SendDate { get; set; }

        public string ReceiveDate { get; set; }

        public string RecallDate { get; set; }

        public string RefundDate { get; set; }

    }
}

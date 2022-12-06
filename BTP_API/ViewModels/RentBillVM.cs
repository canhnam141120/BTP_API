using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class RentBillVM
    {   

        public bool IsRefund { get; set; }

        public DateOnly? RefundDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BTP_API.ViewModels
{
    public class BookVM
    {
        public int CategoryId { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Author { get; set; }
        
        public string Publisher { get; set; }
        
        public int Year { get; set; }

        public string Language { get; set; }
        
        public int NumberOfPages { get; set; }
        
        public float Weight { get; set; }
        
        public float CoverPrice { get; set; }
        
        public float DepositPrice { get; set; }
        
        public string StatusBook { get; set; }

        public string Image { get; set; }
        
        public bool IsExchange { get; set; }
        
        public bool IsRent { get; set; }
        
        public float RentFee { get; set; }
        
    }
}

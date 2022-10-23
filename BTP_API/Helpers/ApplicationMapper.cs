using AutoMapper;

namespace BTP_API.Helpers
{
    public class ApplicationMapper : Profile
    { 
        public ApplicationMapper()
        {
            CreateMap<Book, BookVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Comment, CommentVM>().ReverseMap();
            CreateMap<ExchangeBill, ExchangeBillVM>().ReverseMap();
            CreateMap<ExchangeDetail, ExchangeDetailVM>().ReverseMap();
            CreateMap<Feedback, FeedbackVM>().ReverseMap();
            CreateMap<Fee, FeeVM>().ReverseMap();
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<RentBill, RentBillVM>().ReverseMap();
            CreateMap<RentDetail, RentDetailVM>().ReverseMap();
            CreateMap<ShipInfo, ShipInfoVM>().ReverseMap();
            CreateMap<User, UserVM>().ReverseMap();
        }
    }
}

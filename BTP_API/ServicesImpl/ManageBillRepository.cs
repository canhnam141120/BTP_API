using AutoMapper;

namespace BTP_API.Services
{
    public class ManageBillRepository : IManageBillRepository
    {
        private readonly BTPContext _context;
        private readonly IMapper _mapper;

        public ManageBillRepository(BTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse> getAllExBillAsync(int page = 1)
        {
            var exBills = await _context.ExchangeBills.Include(e => e.User).OrderByDescending(e => e.Id).ToListAsync();
            if (exBills.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<ExchangeBill>.Create(exBills, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> getExBillDetailAsync(int exBillId)
        {
            var exBill = await _context.ExchangeBills.Include(b => b.User).SingleOrDefaultAsync(b => b.Id == exBillId);
            if (exBill == null)
            {
                return new ApiResponse
                {
                    Message = Message.BILL_NOT_EXIST.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exBill,
                NumberOfRecords = 1
            };
        }
        public async Task<ApiMessage> updateStatusExBillDetailAsync(int exBillId, ExchangeBillVM exchangeBillVM)
        {
            var exBill = await _context.ExchangeBills.SingleOrDefaultAsync(b => b.Id == exBillId);
            if (exBill != null)
            {
                exBill = _mapper.Map<ExchangeBill>(exchangeBillVM);
                _context.Update(exBill);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.UPDATE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.BILL_NOT_EXIST.ToString()
            };
        }
        public async Task<ApiResponse> getAllRentBillAsync(int page = 1)
        {
            var rentBills = await _context.RentBills.Include(r => r.User).OrderByDescending(r => r.Id).ToListAsync();
            if (rentBills.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<RentBill>.Create(rentBills, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> getRentBillDetailAsync(int rentBillId)
        {
            var rentBill = await _context.RentBills.Include(b => b.User).SingleOrDefaultAsync(b => b.Id == rentBillId);
            if (rentBill == null)
            {
                return new ApiResponse
                {
                    Message = Message.BILL_NOT_EXIST.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentBill,
                NumberOfRecords = 1
            };
        }
        public async Task<ApiMessage> updateStatusRentBillDetailAsync(int rentBillId, RentBillVM rentBillVM)
        {
            var rentBill = await _context.RentBills.SingleOrDefaultAsync(b => b.Id == rentBillId);
            if (rentBill != null)
            {
                rentBill = _mapper.Map<RentBill>(rentBillVM);
                _context.Update(rentBill);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.UPDATE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.BILL_NOT_EXIST.ToString()
            };
        }
    }
}

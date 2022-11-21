using AutoMapper;

namespace BTP_API.Services
{
    public class ManageFeeRepository : IManageFeeRepository
    {
        private readonly BTPContext _context;
        private readonly IMapper _mapper;

        public ManageFeeRepository(BTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse> getAllFeeAsync()
        {
            var fees = await _context.Fees.Where(p => p.IsActive == true).OrderByDescending(f => f.Id).ToListAsync();
            //var result = PaginatedList<Fee>.Create(fees, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = fees,
                NumberOfRecords = fees.Count
            };
        }

        public async Task<ApiResponse> getFeeByIdAsync(int feeId)
        {
            var fee = await _context.Fees.SingleOrDefaultAsync(p => p.Id == feeId);

            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = fee
            };
        }

        public async Task<ApiResponse> createFeeAsync(FeeVM feeVM)
        {
            var fee = await _context.Fees.SingleOrDefaultAsync(f => f.Code == feeVM.Code && f.IsActive == true);
            if (fee != null)
            {
                fee.IsActive = false;
                _context.Update(fee);
            }
            var feeNew = _mapper.Map<Fee>(feeVM);
            feeNew.IsActive = true;
            _context.Add(feeNew);
            await _context.SaveChangesAsync();
            return new ApiResponse
            {
                Message = Message.CREATE_SUCCESS.ToString(),
                Data = feeNew,
                NumberOfRecords = 1
            };
        }
    }
}

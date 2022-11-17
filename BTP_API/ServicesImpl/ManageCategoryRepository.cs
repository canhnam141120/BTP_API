using AutoMapper;

namespace BTP_API.Services
{
    public class ManageCategoryRepository : IManageCategoryRepository
    {
        private readonly BTPContext _context;
        private readonly IMapper _mapper;

        public ManageCategoryRepository(BTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse> getAllCategoryAsync(int page = 1)
        {
            var categories = await _context.Categories.OrderByDescending(c => c.Id).ToListAsync();
            if (categories.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Category>.Create(categories, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getCategoryByIdAsync(int categoryId)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                return new ApiResponse
                {
                    Message = Message.CATEGORY_NOT_EXIST.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = category,
                NumberOfRecords = 1
            };
        }

        public async Task<ApiResponse> createCategoryAsync(CategoryVM categoryVM)
        {
            var category =_mapper.Map<Category>(categoryVM);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            
            return new ApiResponse
            {
                Message = Message.CREATE_SUCCESS.ToString(),
                Data = category,
                NumberOfRecords = 1
            };
        }

        public async Task<ApiMessage> updateCategoryAsync(int categoryId, CategoryVM categoryVM)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
            if(category != null)
            {
                category.Name = categoryVM.Name;
                _context.Update(category);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.UPDATE_SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.CATEGORY_NOT_EXIST.ToString() };
        }

        public async Task<ApiMessage> deleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.DELETE_SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.CATEGORY_NOT_EXIST.ToString() };
        }
    }
}

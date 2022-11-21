using AutoMapper;
using BTP_API.ViewModels;

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
            var categories = await _context.Categories.Where(c => c.Flag == true).OrderByDescending(c => c.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Categories.Where(c => c.Flag == true).CountAsync();
            //var result = PaginatedList<Category>.Create(categories, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = categories,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getCategoryByIdAsync(int categoryId)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = category
            };
        }

        public async Task<ApiResponse> searchCategoryAsync(string search, int page = 1)
        {
            List<Category> categoris;
            if (search != null)
            {
                search = search.ToLower().Trim();
                categoris = await _context.Categories.Where(b => b.Name.ToLower().Contains(search)).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                categoris = await _context.Categories.OrderByDescending(b => b.Id).ToListAsync();
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = categoris.Skip(10 * (page - 1)).Take(9),
                NumberOfRecords = categoris.Count
            };
        }

        public async Task<ApiResponse> createCategoryAsync(CategoryVM categoryVM)
        {
            var category = new Category
            {
                Name = categoryVM.Name,
                Flag = true
            };
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
                category.Flag = false;
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.DELETE_SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.CATEGORY_NOT_EXIST.ToString() };
        }
    }
}

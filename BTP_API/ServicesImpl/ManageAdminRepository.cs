namespace BTP_API.Services
{
    public class ManageAdminRepository : IManageAdminRepository
    {
        private readonly BTPContext _context;

        public ManageAdminRepository(BTPContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> getAllAdminAsync(int page = 1)
        {
            var admins = await _context.Users.Where(b => b.RoleId == 2 && b.IsActive == true).OrderByDescending(b => b.Id).ToListAsync();
            if (admins.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<User>.Create(admins, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiMessage> removeAdminAsync(int userId)
        {
            var admin = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if(admin != null)
            {
                admin.RoleId = 3;
                _context.Update(admin);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.USER_NOT_EXIST.ToString()};
        }

        public async Task<ApiResponse> searchAdminAsync(string search, int page = 1)
        {
            List<User> admins;

            if (search != null)
            {
                search = search.ToLower().Trim();
                admins = await _context.Users.Where(b => b.RoleId == 2 && b.Email.ToLower().Contains(search) || b.RoleId == 2 && b.Phone.Contains(search)).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                admins = await _context.Users.Where(b => b.RoleId == 2 && b.IsActive == true).OrderByDescending(b => b.Id).ToListAsync();
            }
            
            if (admins.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<User>.Create(admins, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
    }
}

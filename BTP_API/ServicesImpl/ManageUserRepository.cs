namespace BTP_API.ServicesImpl
{
    public class ManageUserRepository : IManageUserRepository
    {
        private readonly BTPContext _context;

        public ManageUserRepository(BTPContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> getAllUserAsync(int page = 1)
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.IsVerify == true).OrderByDescending(u => u.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Users.Where(b => b.RoleId == 3 && b.IsVerify == true).CountAsync();
            //var result = PaginatedList<User>.Create(users, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getAllUserActiveAsync(int page = 1)
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == true && b.IsVerify == true).OrderByDescending(u => u.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == true && b.IsVerify == true).CountAsync();
            //var result = PaginatedList<User>.Create(users, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getAllUserBanAsync(int page = 1)
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == false && b.IsVerify == true).OrderByDescending(u => u.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == false && b.IsVerify == true).CountAsync();
            //var result = PaginatedList<User>.Create(users, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getTopUserLikeAsync()
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == true && b.IsVerify == true).OrderByDescending(b => b.LikeNumber).Take(6).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users,
                NumberOfRecords = users.Count
            };
        }

        public async Task<ApiResponse> getUserByIdAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(b => b.RoleId == 3 && b.Id == userId && b.IsVerify == true);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = user
            };
        }

        public async Task<ApiResponse> searchUserAsync(string search, int page = 1)
        {
            List<User> users;
            if(search != null)
            {
                search = search.ToLower().Trim();
                users = await _context.Users.Where(b => b.RoleId == 3 && b.Email.ToLower().Contains(search) && b.IsVerify == true || b.RoleId == 3 && b.Phone.Contains(search) && b.IsVerify == true).OrderByDescending(u => u.Id).ToListAsync();
            }
            else
            {
                users = await _context.Users.Where(b => b.RoleId == 3).OrderByDescending(u => u.Id).ToListAsync();
            }
            //var result = PaginatedList<User>.Create(users, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users.Skip(10*(page-1)).Take(10),
                NumberOfRecords = users.Count
            };
        }

        public async Task<ApiMessage> banAccAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(b => b.RoleId == 3 && b.Id == userId);
            if(user != null)
            {
                user.IsActive = false;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.USER_NOT_EXIST.ToString()
            };
        }

        public async Task<ApiMessage> activeAccAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(b => b.RoleId == 3 && b.Id == userId);
            if (user != null)
            {
                user.IsActive = true;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.USER_NOT_EXIST.ToString()
            };
        }

        public async Task<ApiMessage> authorityAdminAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(b => b.RoleId == 3 && b.Id == userId);
            if (user != null)
            {
                user.RoleId = 2;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.USER_NOT_EXIST.ToString()
            };
        }
    }
}

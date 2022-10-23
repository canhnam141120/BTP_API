using BTP_API.Models;

namespace BTP_API.ServicesImpl
{
    public class ManageUserRepository : IManageUserRepository
    {
        private readonly BTPContext _context;

        public ManageUserRepository(BTPContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> getAllUserAsync()
        {
            var users = await _context.Users.Where(b => b.RoleId == 3).ToListAsync();
            if (users.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users,
                NumberOfRecords = users.Count
            };
        }

        public async Task<ApiResponse> getAllUserActiveAsync()
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == true).ToListAsync();
            if (users.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users,
                NumberOfRecords = users.Count
            };
        }

        public async Task<ApiResponse> getAllUserBanAsync()
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == false).ToListAsync();
            if (users.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users,
                NumberOfRecords = users.Count
            };
        }

        public async Task<ApiResponse> getUserByIdAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(b => b.RoleId == 3 && b.Id == userId);
            if (user == null)
            {
                return new ApiResponse
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = user,
                NumberOfRecords = 1
            };
        }

        public async Task<ApiResponse> searchUserAsync(string search)
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.Email.Contains(search) || b.RoleId == 3 && b.Phone.Contains(search)).ToListAsync();
            if (users.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users,
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

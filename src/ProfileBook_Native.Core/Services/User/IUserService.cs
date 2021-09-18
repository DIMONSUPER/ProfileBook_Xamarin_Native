using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileBook_Native.Core.Models;

namespace ProfileBook_Native.Core.Services.User
{
    public interface IUserService
    {
        Task<int> SaveUserAsync(UserModel user);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
    }
}

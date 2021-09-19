using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileBook_Native.Core.Models;

namespace ProfileBook_Native.Core.Services.User
{
    public interface IUserService
    {
        Task<int> SaveUserAsync(UserModel user);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        bool IsAuthCompleted { get; set; }
        bool IsRememberMe { get; set; }
        bool IsDarkModeOn { get; set; }
        string Language { get; set; }
        int UserId { get; set; }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileBook_Native.Core.Models;
using static ProfileBook_Native.Core.Services.Repository.IRepositoryService;

namespace ProfileBook_Native.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepositoryService _repositoryService;

        public UserService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        #region -- IUserService implementation --

        public Task<List<UserModel>> GetAllUsersAsync()
        {
            return _repositoryService.GetAllAsync<UserModel>();
        }

        public Task<UserModel> GetUserByIdAsync(int id)
        {
            return _repositoryService.GetByIdAsync<UserModel>(id);
        }

        public Task<int> SaveUserAsync(UserModel user)
        {
            return _repositoryService.InsertAsync(user);
        }

        #endregion
    }
}

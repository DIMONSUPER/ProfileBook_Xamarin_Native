using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Services.Repository;
using ProfileBook_Native.Core.Services.Settings;

namespace ProfileBook_Native.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepositoryService _repositoryService;
        private readonly ISettingsService _settingsService;

        public UserService(
            IRepositoryService repositoryService,
            ISettingsService settingsService)
        {
            _repositoryService = repositoryService;
            _settingsService = settingsService;
        }

        #region -- IUserService implementation --

        public bool IsAuthCompleted
        {
            get => _settingsService.IsAuthCompleted;
            set => _settingsService.IsAuthCompleted = value;
        }

        public bool IsRememberMe
        {
            get => _settingsService.IsRememberMe;
            set => _settingsService.IsRememberMe = value;
        }

        public bool IsDarkModeOn
        {
            get => _settingsService.IsDarkModeOn;
            set => _settingsService.IsDarkModeOn = value;
        }

        public string Language
        {
            get => _settingsService.Language;
            set => _settingsService.Language = value;
        }

        public int UserId
        {
            get => _settingsService.UserId;
            set => _settingsService.UserId = value;
        }

        public Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return _repositoryService.GetAllAsync<UserModel>();
        }

        public Task<UserModel> GetUserByIdAsync(int id)
        {
            return _repositoryService.GetSingleByIdAsync<UserModel>(id);
        }

        public Task<int> SaveUserAsync(UserModel user)
        {
            return _repositoryService.SaveAsync(user);
        }

        #endregion
    }
}

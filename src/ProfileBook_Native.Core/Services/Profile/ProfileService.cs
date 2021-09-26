using System.Collections.Generic;
using System.Threading.Tasks;
using ODBmobile.Helpers;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Services.Repository;

namespace ProfileBook_Native.Core.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IRepositoryService _repositoryService;

        public ProfileService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        #region -- IProfileService implementation --

        public Task<AOResult<IEnumerable<ProfileModel>>> GetAllProfilesAsync() =>
            AOResult.ExecuteTaskAsync(_ => _repositoryService.GetAllAsync<ProfileModel>());

        public Task<AOResult<int>> SaveOrUpdateProfileAsync(ProfileModel profileModel) =>
            AOResult.ExecuteTaskAsync(_ => _repositoryService.SaveOrUpdateAsync(profileModel));

        #endregion
    }
}

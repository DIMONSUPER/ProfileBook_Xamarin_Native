using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<IEnumerable<ProfileModel>> GetAllProfilesAsync()
        {
            return _repositoryService.GetAllAsync<ProfileModel>();
        }

        public Task<int> SaveOrUpdateProfileAsync(ProfileModel profileModel)
        {
            return _repositoryService.SaveOrUpdateAsync(profileModel);
        }

        #endregion
    }
}

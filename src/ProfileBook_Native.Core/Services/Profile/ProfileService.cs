using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileBook_Native.Core.Models;
using static ProfileBook_Native.Core.Services.Repository.IRepositoryService;

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

        public Task<List<ProfileModel>> GetAllProfilesAsync()
        {
            return _repositoryService.GetAllAsync<ProfileModel>();
        }

        public Task<int> SaveProfileAsync(ProfileModel profileModel)
        {
            return _repositoryService.InsertAsync(profileModel);
        }

        #endregion
    }
}

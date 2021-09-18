using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileBook_Native.Core.Models;

namespace ProfileBook_Native.Core.Services.Profile
{
    public interface IProfileService
    {
        Task<List<ProfileModel>> GetAllProfilesAsync();
        Task<int> SaveProfileAsync(ProfileModel profileModel);
    }
}

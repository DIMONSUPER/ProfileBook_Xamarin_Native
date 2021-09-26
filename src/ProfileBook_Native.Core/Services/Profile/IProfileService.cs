using System.Collections.Generic;
using System.Threading.Tasks;
using ODBmobile.Helpers;
using ProfileBook_Native.Core.Models;

namespace ProfileBook_Native.Core.Services.Profile
{
    public interface IProfileService
    {
        Task<AOResult<IEnumerable<ProfileModel>>> GetAllProfilesAsync();
        Task<AOResult<int>> SaveOrUpdateProfileAsync(ProfileModel profileModel);
    }
}

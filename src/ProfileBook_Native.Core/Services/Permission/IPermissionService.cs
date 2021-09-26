using System.Threading.Tasks;
using ODBmobile.Helpers;

namespace ProfileBook_Native.Core.Services.Permission
{
    public interface IPermissionService
    {
        Task<AOResult<bool>> RequestCameraPermission();

        Task<AOResult<bool>> RequestStoragePermission();

        Task<AOResult<bool>> RequestPhotosPermission();
    }
}

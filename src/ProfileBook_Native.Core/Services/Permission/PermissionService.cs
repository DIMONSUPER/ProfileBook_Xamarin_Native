using System;
using System.Threading.Tasks;
using ODBmobile.Helpers;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace ProfileBook_Native.Core.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissions _permissions;

        public PermissionService(IPermissions permissions)
        {
            _permissions = permissions;
        }

        #region -- IPermissionService implementation --

        public Task<AOResult<bool>> RequestCameraPermission() =>
            AOResult.ExecuteTaskAsync(async onFailure =>
            {
                var result = false;

                var permissionResult = await _permissions.CheckPermissionStatusAsync<CameraPermission>();

                result = permissionResult != PermissionStatus.Granted
                    ? await _permissions.RequestPermissionAsync<CameraPermission>() == PermissionStatus.Granted
                    : true;

                return result;
            });

        public Task<AOResult<bool>> RequestStoragePermission() =>
            AOResult.ExecuteTaskAsync(async onFailure =>
            {
                var result = false;

                var permissionResult = await _permissions.CheckPermissionStatusAsync<StoragePermission>();

                result = permissionResult != PermissionStatus.Granted
                    ? await _permissions.RequestPermissionAsync<StoragePermission>() == PermissionStatus.Granted
                    : true;

                return result;
            });

        public Task<AOResult<bool>> RequestPhotosPermission() =>
            AOResult.ExecuteTaskAsync(async onFailure =>
            {
                var result = false;

                var permissionResult = await _permissions.CheckPermissionStatusAsync<PhotosPermission>();

                result = permissionResult != PermissionStatus.Granted
                    ? await _permissions.RequestPermissionAsync<PhotosPermission>() == PermissionStatus.Granted
                    : true;

                return result;
            });

        #endregion
    }
}

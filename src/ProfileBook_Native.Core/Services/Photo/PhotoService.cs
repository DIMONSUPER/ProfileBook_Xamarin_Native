using System.Threading.Tasks;
using ODBmobile.Helpers;
using Plugin.Media.Abstractions;

namespace ProfileBook_Native.Core.Services.Photo
{
    public class PhotoService : IPhotoService
    {
        private readonly IMedia _media;

        public PhotoService(IMedia media)
        {
            _media = media;
        }

        #region -- IPhotoService implementation --

        public Task<AOResult<MediaFile>> PickPhotoAsync() =>
            AOResult.ExecuteTaskAsync(async onFailure =>
            {
                MediaFile result = null;

                if (_media.IsPickPhotoSupported)
                {
                    result = await _media.PickPhotoAsync(new() { CompressionQuality = 30 });
                }

                if (result == null)
                {
                    onFailure("MediaFile is null");
                }

                return result;
            });

        public Task<AOResult<MediaFile>> TakePhotoAsync() =>
            AOResult.ExecuteTaskAsync(async onFailure =>
            {
                MediaFile result = null;

                if (_media.IsTakePhotoSupported)
                {
                    result = await _media.TakePhotoAsync(new() { CompressionQuality = 30 });
                }

                if (result == null)
                {
                    onFailure("MediaFile is null");
                }

                return result;
            });

        #endregion
    }
}

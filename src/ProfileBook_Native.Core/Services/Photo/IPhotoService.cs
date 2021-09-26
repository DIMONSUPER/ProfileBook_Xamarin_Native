using System.Threading.Tasks;
using ODBmobile.Helpers;
using Plugin.Media.Abstractions;

namespace ProfileBook_Native.Core.Services.Photo
{
    public interface IPhotoService
    {
        Task<AOResult<MediaFile>> PickPhotoAsync();

        Task<AOResult<MediaFile>> TakePhotoAsync();
    }
}

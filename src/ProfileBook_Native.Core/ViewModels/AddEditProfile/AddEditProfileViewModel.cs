using System;
using System.IO;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.Services.MapperService;
using ProfileBook_Native.Core.Services.Permission;
using ProfileBook_Native.Core.Services.Photo;
using ProfileBook_Native.Core.Services.Profile;
using ProfileBook_Native.Core.Services.User;

namespace ProfileBook_Native.Core.ViewModels.AddEditProfile
{
    public class AddEditProfileViewModel : BaseViewModel<ProfileBindableModel, ProfileBindableModel>
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly IMapperService _mapperService;
        private readonly IPhotoService _photoService;
        private readonly IPermissionService _permissionService;
        private readonly IUserDialogs _userDialogs;

        private bool _isEditMode;

        public AddEditProfileViewModel(
            IMvxNavigationService navigationService,
            IProfileService profileService,
            IUserService userService,
            IMapperService mapperService,
            IPhotoService photoService,
            IPermissionService permissionService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _profileService = profileService;
            _userService = userService;
            _mapperService = mapperService;
            _photoService = photoService;
            _permissionService = permissionService;
            _userDialogs = userDialogs;
        }

        #region -- Public properties --

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _canExecute;
        public bool CanExecute
        {
            get => _canExecute;
            set => SetProperty(ref _canExecute, value);
        }

        private ProfileBindableModel _currentProfile;
        public ProfileBindableModel CurrentProfile
        {
            get => _currentProfile;
            set => SetProperty(ref _currentProfile, value);
        }

        private IMvxCommand _saveButtonTappedCommand;
        public IMvxCommand SaveButtonTappedCommand => _saveButtonTappedCommand ??= new MvxCommand(OnSaveButtonTappedCommandAsync, () => CanExecute);

        private IMvxCommand _profileImageTappedCommand;
        public IMvxCommand ProfileImageTappedCommand => _profileImageTappedCommand ??= new MvxCommand(OnProfileImageTappedCommandAsync);

        #endregion

        #region -- Overrides --

        public override void ViewCreated()
        {
            base.ViewCreated();

            CurrentProfile.PropertyChanged += OnCurrenProfilePropertyChanged;
        }

        public override Task Initialize()
        {
            Title = _isEditMode ? Strings.EditProfile : Strings.AddProfile;

            if (CurrentProfile is null)
            {
                CurrentProfile = new ProfileBindableModel
                {
                    UserId = _userService.CurrentUserId,
                };
            }

            return Task.CompletedTask;
        }

        public override void Prepare(ProfileBindableModel parameter)
        {
            _isEditMode = true;
            CurrentProfile = parameter;
            CanExecute = !string.IsNullOrWhiteSpace(CurrentProfile?.NickName) && !string.IsNullOrWhiteSpace(CurrentProfile?.Name);
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing)
            {
                CurrentProfile.PropertyChanged -= OnCurrenProfilePropertyChanged;
            }

            base.ViewDestroy(viewFinishing);
        }

        #endregion

        #region -- Private helpers --

        private void OnProfileImageTappedCommandAsync()
        {
            _userDialogs.ActionSheet(new ActionSheetConfig()
                .Add(Strings.PickFromGallery, PickPhotoAsync, icon: "ic_collections.png")
                .Add(Strings.TakeAPhoto, TakePhotoAsync, icon: "ic_camera_alt.png")
                .SetCancel(Strings.Cancel));
        }

        private async void PickPhotoAsync()
        {
            var photoPermissionRequest = await _permissionService.RequestPhotosPermission();
            var storagePermissionRequest = await _permissionService.RequestStoragePermission();

            var isPermissionsGranted = photoPermissionRequest.IsSuccess && photoPermissionRequest.Result
                                       && storagePermissionRequest.IsSuccess && storagePermissionRequest.Result;

            if (isPermissionsGranted)
            {
                var photoRequest = await _photoService.PickPhotoAsync();

                if (photoRequest.IsSuccess)
                {
                    using var stream = photoRequest.Result.GetStream();
                    using var memoryStream = new MemoryStream();

                    await stream.CopyToAsync(memoryStream);

                    CurrentProfile.ProfileImage = memoryStream.ToArray();
                }
            }
        }

        private async void TakePhotoAsync()
        {
            var cameraPermissionRequest = await _permissionService.RequestCameraPermission();
            var storagePermissionRequest = await _permissionService.RequestStoragePermission();

            var isPermissionsGranted = cameraPermissionRequest.IsSuccess && cameraPermissionRequest.Result
                                       && storagePermissionRequest.IsSuccess && storagePermissionRequest.Result;

            if (isPermissionsGranted)
            {
                var photoRequest = await _photoService.TakePhotoAsync();

                if (photoRequest.IsSuccess)
                {
                    using var stream = photoRequest.Result.GetStream();
                    using var memoryStream = new MemoryStream();

                    await stream.CopyToAsync(memoryStream);

                    CurrentProfile.ProfileImage = memoryStream.ToArray();
                }
            }
        }

        private void OnCurrenProfilePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentProfile.NickName) || e.PropertyName == nameof(CurrentProfile.Name))
            {
                CanExecute = !string.IsNullOrWhiteSpace(CurrentProfile?.NickName) && !string.IsNullOrWhiteSpace(CurrentProfile?.Name);
            }
        }

        private async void OnSaveButtonTappedCommandAsync()
        {
            if (!_isEditMode)
            {
                CurrentProfile.Date = DateTime.Now;
            }

            var profileModel = await _mapperService.MapAsync<ProfileModel>(CurrentProfile);

            var saveRequest= await _profileService.SaveOrUpdateProfileAsync(profileModel);

            if (saveRequest.IsSuccess)
            {
                CurrentProfile.Id = saveRequest.Result;
            }

            await NavigationService.Close(this, CurrentProfile);
        }

        #endregion
    }
}

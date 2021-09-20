using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.Services.MapperService;
using ProfileBook_Native.Core.Services.Profile;
using ProfileBook_Native.Core.Services.User;

namespace ProfileBook_Native.Core.ViewModels.AddEditProfile
{
    public class AddEditProfileViewModel : BaseViewModel<ProfileBindableModel, ProfileBindableModel>
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly IMapperService _mapperService;

        public AddEditProfileViewModel(
            IMvxNavigationService navigationService,
            IProfileService profileService,
            IUserService userService,
            IMapperService mapperService)
            : base(navigationService)
        {
            _profileService = profileService;
            _userService = userService;
            _mapperService = mapperService;
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

        #endregion

        #region -- Overrides --

        public override Task Initialize()
        {
            if (CurrentProfile is null)
            {
                Title = Strings.AddProfile;

                CurrentProfile = new ProfileBindableModel
                {
                    UserId = _userService.UserId,
                };

                CurrentProfile.PropertyChanged += OnCurrenProfilePropertyChanged;
            }

            return Task.CompletedTask;
        }

        private void OnCurrenProfilePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentProfile.NickName) || e.PropertyName == nameof(CurrentProfile.Name))
            {
                CanExecute = !string.IsNullOrWhiteSpace(CurrentProfile?.NickName) && !string.IsNullOrWhiteSpace(CurrentProfile?.Name);
            }
        }

        public override void Prepare(ProfileBindableModel parameter)
        {
            Title = Strings.EditProfile;
            CurrentProfile = parameter;
            CanExecute = !string.IsNullOrWhiteSpace(CurrentProfile?.NickName) && !string.IsNullOrWhiteSpace(CurrentProfile?.Name);
        }

        #endregion

        #region -- Private helpers --

        private async void OnSaveButtonTappedCommandAsync()
        {
            CurrentProfile.Date = System.DateTime.Now;

            var profileModel = await _mapperService.MapAsync<ProfileModel>(CurrentProfile);

            CurrentProfile.Id = await _profileService.SaveOrUpdateProfileAsync(profileModel);

            await NavigationService.Close(this, CurrentProfile);
        }

        #endregion
    }
}

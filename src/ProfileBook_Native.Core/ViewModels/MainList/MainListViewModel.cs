using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Services.MapperService;
using ProfileBook_Native.Core.Services.Profile;
using ProfileBook_Native.Core.Services.User;
using ProfileBook_Native.Core.ViewModels.AddEditProfile;
using ProfileBook_Native.Core.ViewModels.SignIn;

namespace ProfileBook_Native.Core.ViewModels.MainList
{
    public class MainListViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly IMapperService _mapperService;

        public MainListViewModel(
            IMvxNavigationService navigationService,
            IProfileService profileService,
            IUserService userService,
            IMapperService mapperService)
            : base(navigationService)
        {
            _profileService = profileService;
            _userService = userService;
            _mapperService = mapperService;

            Profiles = new();
        }

        #region -- Public properties --

        private ObservableCollection<ProfileBindableModel> _profiles;
        public ObservableCollection<ProfileBindableModel> Profiles
        {
            get => _profiles;
            set => SetProperty(ref _profiles, value);
        }

        private bool _hasProfiles;
        public bool HasProfiles
        {
            get => _hasProfiles;
            set => SetProperty(ref _hasProfiles, value);
        }

        private ICommand _logOutButtonTappedCommand;
        public ICommand LogOutButtonTappedCommand => _logOutButtonTappedCommand ??= new MvxCommand(OnLogOutButtonTappedCommandAsync);

        private ICommand _settingsButtonTappedCommand;
        public ICommand SettingsButtonTappedCommand => _settingsButtonTappedCommand ??= new MvxCommand(OnSettingsButtonTappedCommand);

        private ICommand _editButtonTappedCommand;
        public ICommand EditButtonTappedCommand => _editButtonTappedCommand ??= new MvxCommand<ProfileBindableModel>(OnEditButtonTappedCommand);

        private ICommand _deleteButtonTappedCommand;
        public ICommand DeleteButtonTappedCommand => _deleteButtonTappedCommand ??= new MvxCommand<ProfileBindableModel>(OnDeleteButtonTappedCommandAsync);

        private ICommand _profileTappedCommand;
        public ICommand ProfileTappedCommand => _profileTappedCommand ??= new MvxCommand<ProfileBindableModel>(OnProfileTappedCommandAsync);

        private ICommand _addButtonTappedCommand;
        public ICommand AddButtonTappedCommand => _addButtonTappedCommand ??= new MvxCommand(OnAddButtonTappedCommandAsync);

        #endregion

        #region -- Overrides --

        public override Task Initialize()
        {
            base.Initialize();

            UpdateProfilesAsync();

            return Task.CompletedTask;
        }

        #endregion

        #region -- Private helpers --

        private async void OnAddButtonTappedCommandAsync()
        {
            var newProfile = await NavigationService.Navigate<ProfileBindableModel>(typeof(AddEditProfileViewModel));

            if (newProfile != null)
            {
                UpdateProfilesAsync();
            }
        }

        private async void OnLogOutButtonTappedCommandAsync()
        {
            _userService.IsRememberMe = false;
            _userService.IsAuthCompleted = false;
            _userService.UserId = -1;

            await Task.WhenAll(NavigationService.Navigate<SignInViewModel>(), NavigationService.Close(this));
        }

        private void OnSettingsButtonTappedCommand()
        { }

        private void OnEditButtonTappedCommand(ProfileBindableModel profile)
        { }

        private void OnDeleteButtonTappedCommandAsync(ProfileBindableModel profile)
        {
            HasProfiles = Profiles.Any();
        }

        private async void OnProfileTappedCommandAsync(ProfileBindableModel profile)
        {
            var editedProfile = await NavigationService.Navigate<ProfileBindableModel, ProfileBindableModel>(typeof(AddEditProfileViewModel), profile);

            if (editedProfile != null)
            {
                UpdateProfilesAsync();
            }
        }

        private async void UpdateProfilesAsync()
        {
            var profiles = await _profileService.GetAllProfilesAsync();

            var profileBindableModels = await _mapperService.MapRangeAsync<ProfileModel, ProfileBindableModel>(profiles, (m, vm) =>
            {
                vm.TapCommad = ProfileTappedCommand;
                vm.EditCommad = EditButtonTappedCommand;
                vm.DeleteCommand = DeleteButtonTappedCommand;
            });

            Profiles = new(profileBindableModels.Where(x => x.UserId == _userService.UserId));

            HasProfiles = Profiles.Any();
        }

        #endregion
    }
}

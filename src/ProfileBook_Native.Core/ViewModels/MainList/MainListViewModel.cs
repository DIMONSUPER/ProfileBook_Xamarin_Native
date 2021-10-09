using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using ProfileBook_Native.Core.Enums;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.Services.MapperService;
using ProfileBook_Native.Core.Services.Profile;
using ProfileBook_Native.Core.Services.Theme;
using ProfileBook_Native.Core.Services.User;
using ProfileBook_Native.Core.ViewModels.AddEditProfile;
using ProfileBook_Native.Core.ViewModels.Settings;
using ProfileBook_Native.Core.ViewModels.SignIn;

namespace ProfileBook_Native.Core.ViewModels.MainList
{
    public class MainListViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly IMapperService _mapperService;
        private readonly IUserDialogs _userDialogs;
        private readonly IThemeService _themeService;

        public MainListViewModel(
            IMvxNavigationService navigationService,
            IProfileService profileService,
            IUserService userService,
            IMapperService mapperService,
            IUserDialogs userDialogs,
            IThemeService themeService)
            : base(navigationService)
        {
            _profileService = profileService;
            _userService = userService;
            _mapperService = mapperService;
            _userDialogs = userDialogs;
            _themeService = themeService;

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
        public ICommand SettingsButtonTappedCommand => _settingsButtonTappedCommand ??= new MvxCommand(OnSettingsButtonTappedCommandAsync);

        private ICommand _deleteButtonTappedCommand;
        public ICommand DeleteButtonTappedCommand => _deleteButtonTappedCommand ??= new MvxCommand<ProfileBindableModel>(OnDeleteButtonTappedCommandAsync);

        private ICommand _profileTappedCommand;
        public ICommand ProfileTappedCommand => _profileTappedCommand ??= new MvxCommand<ProfileBindableModel>(OnProfileTappedCommandAsync);

        private ICommand _profileAvatarTappedCommand;
        public ICommand ProfileAvatarTappedCommand => _profileAvatarTappedCommand ??= new MvxCommand<ProfileBindableModel>(OnProfileAvatarTappedCommandAsync);

        private ICommand _addButtonTappedCommand;
        public ICommand AddButtonTappedCommand => _addButtonTappedCommand ??= new MvxCommand(OnAddButtonTappedCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void ViewCreated()
        {
            base.ViewCreated();

            _themeService.ChangeThemeTo((ETheme)_userService.Theme);

            await UpdateProfilesAsync();
        }

        public override async void ViewAppearing()
        {
            base.ViewAppearing();

            await UpdateProfilesAsync();
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == nameof(Profiles))
            {
                HasProfiles = Profiles != null && Profiles.Any();
            }
        }

        #endregion

        #region -- Private helpers --

        private void OnProfileAvatarTappedCommandAsync(ProfileBindableModel profile)
        {
        }

        private async void OnAddButtonTappedCommandAsync()
        {
            var newProfile = await NavigationService.Navigate<ProfileBindableModel>(typeof(AddEditProfileViewModel));

            if (newProfile != null)
            {
                await UpdateProfilesAsync();
            }
        }

        private async void OnLogOutButtonTappedCommandAsync()
        {
            var isConfirmed = await _userDialogs.ConfirmAsync(
                Strings.AreYouSureUWantToLogOut,
                okText: Strings.Yes,
                cancelText: Strings.No);

            if (isConfirmed)
            {
                _userService.IsRememberMe = _userService.IsAuthCompleted = false;
                _userService.CurrentUserId = -1;

                await Task.WhenAll(NavigationService.Navigate<SignInViewModel>(), NavigationService.Close(this));
            }
        }

        private async void OnSettingsButtonTappedCommandAsync()
        {
            await NavigationService.Navigate(typeof(SettingsViewModel));
        }

        private async void OnDeleteButtonTappedCommandAsync(ProfileBindableModel profile)
        {
            var isConfirmed = await _userDialogs.ConfirmAsync(
                Strings.AreYouSureYouWantToDelete,
                okText: Strings.Yes,
                cancelText: Strings.No);

            if (isConfirmed)
            {
                Profiles.Remove(profile);
                var profileModel = await _mapperService.MapAsync<ProfileModel>(profile);
                await _profileService.DeleteProfileAsync(profileModel);
            }
        }

        private async void OnProfileTappedCommandAsync(ProfileBindableModel profile)
        {
            var editedProfile = await NavigationService.Navigate<ProfileBindableModel, ProfileBindableModel>(typeof(AddEditProfileViewModel), profile);

            if (editedProfile is not null)
            {
                await UpdateProfilesAsync();
            }
        }

        private async Task UpdateProfilesAsync()
        {
            var profilesRequest = await _profileService.GetAllProfilesAsync();

            if (profilesRequest.IsSuccess)
            {
                var profileBindableModels = await _mapperService.MapRangeAsync<ProfileModel, ProfileBindableModel>(profilesRequest.Result, (m, vm) =>
                {
                    vm.TapCommad = ProfileTappedCommand;
                    vm.AvatarTappedCommad = ProfileAvatarTappedCommand;
                    vm.DeleteCommand = DeleteButtonTappedCommand;
                });

                var sortedProfiles = GetSortedProfiles(profileBindableModels.Where(x => x.UserId == _userService.CurrentUserId));

                Profiles = new(sortedProfiles);
            }
        }

        private IEnumerable<ProfileBindableModel> GetSortedProfiles(IEnumerable<ProfileBindableModel> profiles)
        {
            return (ESortOption)_userService.SortOption switch
            {
                ESortOption.ByDate => profiles.OrderBy(x => x.Date),
                ESortOption.ByName => profiles.OrderBy(x => x.Name),
                ESortOption.ByNickname => profiles.OrderBy(x => x.NickName),
                _ => throw new System.NotImplementedException(),
            };
        }

        #endregion
    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Services.Profile;
using ProfileBook_Native.Core.Services.User;
using ProfileBook_Native.Core.ViewModels.SignIn;

namespace ProfileBook_Native.Core.ViewModels.MainList
{
    public class MainListViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;

        public MainListViewModel(
            IMvxNavigationService navigationService,
            IProfileService profileService,
            IUserService userService)
            : base(navigationService)
        {
            _profileService = profileService;
            _userService = userService;

            Profiles = new();
        }

        #region -- Public properties --

        private ObservableCollection<ProfileBindableModel> _profiles;
        public ObservableCollection<ProfileBindableModel> Profiles
        {
            get => _profiles;
            set => SetProperty(ref _profiles, value);
        }

        private IMvxCommand _logOutButtonTappedCommand;
        public IMvxCommand LogOutButtonTappedCommand => _logOutButtonTappedCommand ??= new MvxCommand(OnLogOutButtonTappedCommand);

        private IMvxCommand _settingsButtonTappedCommand;
        public IMvxCommand SettingsButtonTappedCommand => _settingsButtonTappedCommand ??= new MvxCommand(OnSettingsButtonTappedCommand);

        private IMvxCommand _editButtonTappedCommand;
        public IMvxCommand EditButtonTappedCommand => _editButtonTappedCommand ??= new MvxCommand(OnEditButtonTappedCommand);

        private IMvxCommand _deleteButtonTappedCommand;
        public IMvxCommand DeleteButtonTappedCommand => _deleteButtonTappedCommand ??= new MvxCommand(OnDeleteButtonTappedCommandAsync);

        private IMvxCommand _profileTappedCommand;
        public IMvxCommand ProfileTappedCommand => _profileTappedCommand ??= new MvxCommand(OnProfileTappedCommand);

        #endregion

        #region -- Overrides --

        public override async Task Initialize()
        {
            await base.Initialize();

            await UpdateProfilesAsync();
        }

        #endregion

        #region -- Private helpers --

        private void OnLogOutButtonTappedCommand()
        {
            _userService.IsRememberMe = false;
            _userService.IsAuthCompleted = false;
            _userService.UserId = -1;
            NavigationService.Navigate<SignInViewModel>();
        }

        private void OnSettingsButtonTappedCommand()
        { }

        private void OnEditButtonTappedCommand()
        { }

        private void OnDeleteButtonTappedCommandAsync()
        { }

        private void OnProfileTappedCommand()
        { }

        private async Task UpdateProfilesAsync()
        {
            //var profiles = await _profileService.GetAllProfilesAsync();

            //Profiles = new(profiles.Where(x => x.UserId == _userService.UserId));
            Profiles = new()
            {
                new() { Name ="Dima", NickName = "Dimas", Description = "Description",  Date = DateTime.Now },
                new() { Name ="Dima", NickName = "Dimas", Description = "Description",  Date = DateTime.Now },
                new() { Name ="Dima", NickName = "Dimas", Description = "Description",  Date = DateTime.Now },
                new() { Name ="Dima", NickName = "Dimas", Description = "Description",  Date = DateTime.Now },
                new() { Name ="Dima", NickName = "Dimas", Description = "Description",  Date = DateTime.Now },
                new() { Name ="Dima", NickName = "Dimas", Description = "Description",  Date = DateTime.Now },
                new() { Name ="Dima", NickName = "Dimas", Description = "Description",  Date = DateTime.Now },
            };

            Task.Run(async () =>
            {
                await Task.Delay(5000);
                InvokeOnMainThread(() => Profiles[2].Name = "LOOOOOL");
            });
        }

        #endregion
    }
}

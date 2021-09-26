using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Settings;
using ProfileBook_Native.Core.Services.User;
using ProfileBook_Native.Core.ViewModels.MainList;
using ProfileBook_Native.Core.ViewModels.SignIn;
using Xamarin.Essentials;

namespace ProfileBook_Native.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton(UserDialogs.Instance);
            Mvx.IoCProvider.RegisterSingleton(CrossSettings.Current);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Mvx.IoCProvider.RegisterSingleton(CrossMedia.Current);
                Mvx.IoCProvider.RegisterSingleton(CrossPermissions.Current);
            });

            RegisterCustomAppStart<AppStart>();
        }
    }

    public class AppStart : MvxAppStart
    {
        private readonly IUserService _userService;

        public AppStart(
            IMvxApplication application,
            IMvxNavigationService navigationService,
            IUserService userService)
            : base(application, navigationService)
        {
            _userService = userService;
        }

        #region -- Overrides --

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            if (_userService.IsAuthCompleted)
            {
                NavigationService.Navigate<MainListViewModel>().GetAwaiter().GetResult();
            }
            else
            {
                NavigationService.Navigate<SignInViewModel>().GetAwaiter().GetResult();
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}

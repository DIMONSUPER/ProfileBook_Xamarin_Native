using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Plugin.Settings;
using ProfileBook_Native.Core.Services.Settings;
using ProfileBook_Native.Core.ViewModels.MainList;
using ProfileBook_Native.Core.ViewModels.SignIn;

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

            if (Mvx.IoCProvider.TryResolve(out ISettingsService settingsService))
            {
                if (settingsService.IsAuthCompleted)
                {
                    RegisterAppStart<MainListViewModel>();
                }
                else
                {
                    RegisterAppStart<SignInViewModel>();
                }
            }
        }
    }
}

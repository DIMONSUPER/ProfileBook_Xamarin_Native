using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using ProfileBook_Native.Core.Services.User;
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

            RegisterAppStart<SignInViewModel>();
        }
    }
}

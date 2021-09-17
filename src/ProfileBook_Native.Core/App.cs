using MvvmCross.IoC;
using MvvmCross.ViewModels;
using ProfileBook_Native.Core.ViewModels.Main;

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

            RegisterAppStart<MainViewModel>();
        }
    }
}

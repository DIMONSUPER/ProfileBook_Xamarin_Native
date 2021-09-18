using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ProfileBook_Native.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected IMvxNavigationService NavigationService { get; }

        public BaseViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}

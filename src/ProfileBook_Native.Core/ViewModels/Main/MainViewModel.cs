using MvvmCross.Navigation;

namespace ProfileBook_Native.Core.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(
            IMvxNavigationService navigationService)
            : base(navigationService)
        {
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }
    }
}

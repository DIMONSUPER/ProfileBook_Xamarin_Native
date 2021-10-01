using System.ComponentModel;
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

        #region -- Overrides --

        public override void ViewCreated()
        {
            base.ViewCreated();

            PropertyChanged += OnPropertyChanged;
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing)
            {
                PropertyChanged -= OnPropertyChanged;
            }

            base.ViewDestroy(viewFinishing);
        }

        #endregion

        #region -- Protected properties --

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        #endregion
    }
}

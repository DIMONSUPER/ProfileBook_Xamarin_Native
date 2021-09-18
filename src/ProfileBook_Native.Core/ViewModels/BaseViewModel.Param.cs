using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ProfileBook_Native.Core.ViewModels
{
    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
        where TParameter : notnull
    {
        protected BaseViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
        }

        public abstract void Prepare(TParameter parameter);
    }
}

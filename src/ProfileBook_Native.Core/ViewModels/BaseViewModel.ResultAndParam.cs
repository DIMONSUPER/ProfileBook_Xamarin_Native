using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ProfileBook_Native.Core.ViewModels
{
    public abstract class BaseViewModel<TParameter, TResult> : BaseViewModelResult<TResult>, IMvxViewModel<TParameter, TResult>
        where TParameter : notnull
        where TResult : notnull
    {
        protected BaseViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
        }

        public abstract void Prepare(TParameter parameter);
    }
}

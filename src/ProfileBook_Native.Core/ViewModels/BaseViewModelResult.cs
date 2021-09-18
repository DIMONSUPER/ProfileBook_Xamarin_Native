using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ProfileBook_Native.Core.ViewModels
{
    public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
        where TResult : notnull
    {
        protected BaseViewModelResult(IMvxNavigationService navigationService) : base(navigationService)
        {
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource?.Task.IsCompleted == false && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }
    }
}

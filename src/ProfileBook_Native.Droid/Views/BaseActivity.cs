using Android.OS;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using Plugin.Permissions;

namespace ProfileBook_Native.Droid.Views
{
    public abstract class BaseActivity<TViewModel> : MvxActivity<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected abstract int ActivityLayoutId { get; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(ActivityLayoutId);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

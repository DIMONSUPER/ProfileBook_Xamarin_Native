using Android.OS;
using Android.Runtime;
using MvvmCross;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using ProfileBook_Native.Core.Enums;
using ProfileBook_Native.Core.Services.Theme;
using ProfileBook_Native.Core.Services.User;

namespace ProfileBook_Native.Droid.Views
{
    public abstract class BaseActivity<TViewModel> : MvxActivity<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        protected abstract int ActivityLayoutId { get; }

        #region -- Overrides --

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            base.SetTheme(Resource.Style.AppTheme);
            RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;

            SetContentView(ActivityLayoutId);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion
    }
}

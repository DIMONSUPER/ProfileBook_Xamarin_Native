using Acr.UserDialogs;
using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;

namespace ProfileBook_Native.Droid.Views.Splash
{
    [Activity(
        NoHistory = true,
        MainLauncher = true,
        Label = "@string/app_name",
        Theme = "@style/AppTheme.Splash")]
    public class SplashActivity : MvxSplashScreenActivity
    {
        #region -- Overrides --

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            UserDialogs.Init(this);
        }

        #endregion
    }
}

using Acr.UserDialogs;
using Android.App;
using MvvmCross.Platforms.Android.Views;

namespace ProfileBook_Native.Droid.Views.Splash
{
    [Activity(
        NoHistory = true,
        MainLauncher = true,
        Label = "@string/app_name",
        Theme = "@style/AppTheme.Splash",
        Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/ic_launcher_round")]
    public class SplashActivity : MvxSplashScreenActivity
    {
        #region -- Overrides --

        protected override void OnStart()
        {
            base.OnStart();

            UserDialogs.Init(this);
        }

        #endregion
    }
}

using AndroidX.AppCompat.App;
using ProfileBook_Native.Core.Enums;
using ProfileBook_Native.Core.Services.Theme;

namespace ProfileBook_Native.Droid.Services
{
    public class ThemeService : IThemeService
    {
        #region -- IThemeService implementation --

        public void ChangeThemeTo(ETheme theme)
        {
            AppCompatDelegate.DefaultNightMode =
                theme == ETheme.Dark ?
                AppCompatDelegate.ModeNightYes :
                AppCompatDelegate.ModeNightNo;
        }

        #endregion
    }
}

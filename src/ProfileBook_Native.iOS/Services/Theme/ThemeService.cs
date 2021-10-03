using System.Threading.Tasks;
using ProfileBook_Native.Core.Enums;
using ProfileBook_Native.Core.Services.Theme;
using UIKit;

namespace ProfileBook_Native.iOS.Services.Theme
{
    public class ThemeService : IThemeService
    {
        public ThemeService()
        {
        }

        #region -- IThemeService implementation --

        public void ChangeThemeTo(ETheme theme)
        {
            UIApplication.SharedApplication.KeyWindow.OverrideUserInterfaceStyle = theme == ETheme.Light
                ? UIUserInterfaceStyle.Light : UIUserInterfaceStyle.Dark;
        }

        #endregion
    }
}

using System.Threading.Tasks;
using ProfileBook_Native.Core.Enums;

namespace ProfileBook_Native.Core.Services.Theme
{
    public interface IThemeService
    {
        void ChangeThemeTo(ETheme theme);
    }
}

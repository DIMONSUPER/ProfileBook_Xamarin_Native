using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using ProfileBook_Native.Core.Enums;
using ProfileBook_Native.Core.Services.Theme;
using ProfileBook_Native.Core.Services.User;

namespace ProfileBook_Native.Core.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly IThemeService _themeService;

        public SettingsViewModel(
            IMvxNavigationService navigationService,
            IUserService userService,
            IThemeService themeService)
            : base(navigationService)
        {
            _userService = userService;
            _themeService = themeService;
        }

        #region -- Public properties --

        private ESortOption _selectedSortOption;
        public ESortOption SelectedSortOption
        {
            get => _selectedSortOption;
            set => SetProperty(ref _selectedSortOption, value);
        }

        private string _selectedLanguageName;
        public string SelectedLanguageName
        {
            get => _selectedLanguageName;
            set => SetProperty(ref _selectedLanguageName, value);
        }

        private ETheme _currentTheme;
        public ETheme CurrentTheme
        {
            get => _currentTheme;
            set => SetProperty(ref _currentTheme, value);
        }

        private ObservableCollection<string> _languages;
        public ObservableCollection<string> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        #endregion

        #region -- Overrides --

        public override Task Initialize()
        {
            base.Initialize();

            Languages = new()
            {
                Constants.ENGLISH_DISPLAY_NAME,
                Constants.RUSSIAN_DISPLAY_NAME,
                Constants.UKRAINIAN_DISPLAY_NAME,
            };

            SelectedSortOption = (ESortOption)_userService.SortOption;
            SelectedLanguageName = CultureInfo.CurrentUICulture.DisplayName;
            CurrentTheme = (ETheme)_userService.Theme;

            return Task.CompletedTask;
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(SelectedSortOption):
                    _userService.SortOption = (int)SelectedSortOption;
                    break;
                case nameof(SelectedLanguageName):
                    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = SelectedLanguageName switch
                    {
                        Constants.ENGLISH_DISPLAY_NAME => new(Constants.ENGLISH_CODE),
                        Constants.RUSSIAN_DISPLAY_NAME => new(Constants.RUSSIAN_CODE),
                        Constants.UKRAINIAN_DISPLAY_NAME => new(Constants.UKRAINIAN_CODE),
                        _ => throw new System.NotImplementedException()
                    };
                    _userService.Language = CultureInfo.DefaultThreadCurrentCulture.Name;
                    break;
                case nameof(CurrentTheme):
                    _userService.Theme = (int)CurrentTheme;
                    _themeService.ChangeThemeTo(CurrentTheme);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region -- Private helpers --


        #endregion
    }
}

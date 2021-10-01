using System.ComponentModel;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using ProfileBook_Native.Core.Enums;
using ProfileBook_Native.Core.Services.User;

namespace ProfileBook_Native.Core.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public SettingsViewModel(
            IMvxNavigationService navigationService,
            IUserService userService)
            : base(navigationService)
        {
            _userService = userService;
        }

        #region -- Public properties --

        private ESortOptions _sortOptions;
        public ESortOptions SortOptions
        {
            get => _sortOptions;
            set => SetProperty(ref _sortOptions, value);
        }

        private string _languageName;
        public string LanguageName
        {
            get => _languageName;
            set => SetProperty(ref _languageName, value);
        }

        private bool _isDarkThemeOn;
        public bool IsDarkThemeOn
        {
            get => _isDarkThemeOn;
            set => SetProperty(ref _isDarkThemeOn, value);
        }

        #endregion

        #region -- Overrides --

        public override Task Initialize()
        {
            base.Initialize();

            SortOptions = (ESortOptions)_userService.SortOption;
            LanguageName = _userService.Language;
            IsDarkThemeOn = _userService.IsDarkModeOn;

            return Task.CompletedTask;
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(SortOptions):
                    _userService.SortOption = (int)SortOptions;
                    break;
                case nameof(LanguageName):
                    _userService.Language = LanguageName;
                    break;
                case nameof(IsDarkThemeOn):
                    _userService.IsDarkModeOn = IsDarkThemeOn;
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

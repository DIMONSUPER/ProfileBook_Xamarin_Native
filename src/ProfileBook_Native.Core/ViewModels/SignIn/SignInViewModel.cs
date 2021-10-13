using System.ComponentModel;
using System.Linq;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using ProfileBook_Native.Core.Enums;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.Services.Theme;
using ProfileBook_Native.Core.Services.User;
using ProfileBook_Native.Core.ViewModels.MainList;
using ProfileBook_Native.Core.ViewModels.SignUp;

namespace ProfileBook_Native.Core.ViewModels.SignIn
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly IUserDialogs _userDialogs;
        private readonly IThemeService _themeService;

        public SignInViewModel(
            IMvxNavigationService navigationService,
            IUserService userService,
            IUserDialogs userDialogs,
            IThemeService themeService)
            : base(navigationService)
        {
            _userService = userService;
            _userDialogs = userDialogs;
            _themeService = themeService;
        }

        #region -- Public properties --

        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _isRememberMe;
        public bool IsRememberMe
        {
            get => _isRememberMe;
            set => SetProperty(ref _isRememberMe, value);
        }

        private IMvxCommand _signInButtonTappedCommand;
        public IMvxCommand SignInButtonTappedCommand => _signInButtonTappedCommand ??= new MvxCommand(OnSignInButtonTappedCommandAsync, CanExecute);

        private IMvxCommand _signUpButtonTappedCommand;
        public IMvxCommand SignUpButtonTappedCommand => _signUpButtonTappedCommand ??= new MvxCommand(OnSignUpButtonTappedCommandAsync);

        #endregion

        #region -- Overrides --

        public override void ViewCreated()
        {
            base.ViewCreated();

            _themeService.ChangeThemeTo((ETheme)_userService.Theme);
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == nameof(Login) ||
                e.PropertyName == nameof(Password))
            {
                SignInButtonTappedCommand?.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region -- Private helpers --

        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(Login) &&
                   !string.IsNullOrEmpty(Password);
        }

        private async void OnSignInButtonTappedCommandAsync()
        {
            var users = await _userService.GetAllUsersAsync();

            if (users.Any(x => x.Login == Login &&
                               x.Password == Password))
            {
                _userService.IsRememberMe = IsRememberMe;
                _userService.IsAuthCompleted = true;
                _userService.CurrentUserId = users.FirstOrDefault(x => x.Login == Login).Id;
                await NavigationService.Navigate<MainListViewModel>();
            }
            else
            {
                _userDialogs.Alert(Strings.InvalidLoginOrPassword);
                Password = string.Empty;
            }
        }

        private async void OnSignUpButtonTappedCommandAsync()
        {
            var userModel = await NavigationService.Navigate<SignUpViewModel, UserModel>();

            if (userModel is not null)
            {
                Login = userModel.Login;
            }
        }

        #endregion
    }
}

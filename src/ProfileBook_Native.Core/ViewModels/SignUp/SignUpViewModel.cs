using System.Linq;
using System.Text;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.Services.User;

namespace ProfileBook_Native.Core.ViewModels.SignUp
{
    public class SignUpViewModel : BaseViewModelResult<UserModel>
    {
        private readonly IUserService _userService;
        private readonly IUserDialogs _userDialogs;

        public SignUpViewModel(
            IMvxNavigationService navigationService,
            IUserService userService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _userService = userService;
            _userDialogs = userDialogs;
        }

        #region -- Public properties --

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value);
                SignUpButtonTappedCommand?.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                SignUpButtonTappedCommand?.RaiseCanExecuteChanged();
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                SetProperty(ref _confirmPassword, value);
                SignUpButtonTappedCommand?.RaiseCanExecuteChanged();
            }
        }

        private IMvxCommand _signUpButtonTappedCommand;
        public IMvxCommand SignUpButtonTappedCommand => _signUpButtonTappedCommand ??= new MvxCommand(OnSignUpButtonTappedCommandAsync, CanExecute);

        #endregion

        #region -- Private helpers --

        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);
        }

        private async void OnSignUpButtonTappedCommandAsync()
        {
            var users = await _userService.GetAllUsersAsync();

            if (!users.Any(x => x.Login == Login))
            {
                var loginMsg = GetValidationLoginMessage();

                if (string.IsNullOrEmpty(loginMsg))
                {
                    var passwordMsg = GetValidationPasswordMessage();

                    if (string.IsNullOrEmpty(passwordMsg))
                    {
                        var newUser = new UserModel { Login = Login, Password = Password };
                        await _userService.SaveUserAsync(newUser);
                        await NavigationService.Close(this, newUser);
                    }
                    else
                    {
                        _userDialogs.Alert(passwordMsg);
                    }
                }
                else
                {
                    _userDialogs.Alert(loginMsg);
                }
            }
            else
            {
                _userDialogs.Alert(Strings.LoginWarning2);
            }
        }

        private string GetValidationLoginMessage()
        {
            var builder = new StringBuilder();

            builder.Append((Login.Length < 4 || Login.Length > 16) ? Strings.LoginWarning1 + '\n' : string.Empty);
            builder.Append(char.IsDigit(Login[0]) ? Strings.LoginWarning3 : string.Empty);

            return builder.ToString();
        }

        private string GetValidationPasswordMessage()
        {
            var builder = new StringBuilder();

            builder.Append((Password.Length < 8 || Login.Length > 16) ? Strings.PasswordWarning1 + '\n' : string.Empty);
            builder.Append((Password != ConfirmPassword) ? Strings.ConfirmPasswordWarning + '\n' : string.Empty);
            builder.Append((!Password.Any(x => char.IsUpper(x)) || !Password.Any(x => char.IsLower(x)) || !Password.Any(x => char.IsDigit(x)))
                ? Strings.PasswordWarning2
                : string.Empty);

            return builder.ToString();
        }

        #endregion
    }
}

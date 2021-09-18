using System.Linq;
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
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                SetProperty(ref _confirmPassword, value);
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }

        private IMvxCommand _signUpCommand;
        public IMvxCommand SignUpCommand => _signUpCommand ??= new MvxCommand(OnSignUpCommandAsync, CanExecute);

        #endregion

        #region -- Private helpers --

        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);
        }

        private async void OnSignUpCommandAsync()
        {
            var users = await _userService.GetAllUsersAsync();

            if (users.Any(x => x.Login == Login))
            {
                _userDialogs.Alert(Strings.LoginWarning2);
            }
            else
            {
                var newUser = new UserModel { Login = Login, };
                await _userService.SaveUserAsync(newUser);
                await NavigationService.Close(this, newUser);
            }
        }

        

        #endregion
    }
}

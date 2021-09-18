using MvvmCross.Commands;
using MvvmCross.Navigation;
using ProfileBook_Native.Core.ViewModels.SignUp;

namespace ProfileBook_Native.Core.ViewModels.SignIn
{
    public class SignInViewModel : BaseViewModel
    {
        public SignInViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        private IMvxCommand _signInCommand;
        public IMvxCommand SignInCommand => _signInCommand ??= new MvxCommand(OnSignInCommand, CanExecute);

        private IMvxCommand _signUpCommand;
        public IMvxCommand SignUpCommand => _signUpCommand ??= new MvxCommand(OnSignUpCommand);

        #endregion

        #region -- Private helpers --
        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }

        private void OnSignInCommand()
        {
        }

        private void OnSignUpCommand()
        {
            NavigationService.Navigate<SignUpViewModel>();
        }

        #endregion
    }
}

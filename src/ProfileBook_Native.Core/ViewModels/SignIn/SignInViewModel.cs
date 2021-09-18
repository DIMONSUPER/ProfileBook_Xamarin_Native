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
            PropertyChanged += OnPropertyChanged;
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

        private bool _isSignInButtonEnabled;
        public bool IsSignInButtonEnabled
        {
            get => _isSignInButtonEnabled;
            set => SetProperty(ref _isSignInButtonEnabled, value);
        }

        private IMvxCommand _signInCommand;
        public IMvxCommand SignInCommand => _signInCommand ??= new MvxCommand(OnSignInCommand);

        private IMvxCommand _signUpCommand;
        public IMvxCommand SignUpCommand => _signUpCommand ??= new MvxCommand(OnSignUpCommand);

        #endregion

        #region -- Overrides --

        public override void ViewDestroy(bool viewFinishing = true)
        {
            PropertyChanged -= OnPropertyChanged;
            base.ViewDestroy(viewFinishing);
        }

        #endregion

        #region -- Private helpers --

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Login) || e.PropertyName == nameof(Password))
            {
                IsSignInButtonEnabled = !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
            }
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

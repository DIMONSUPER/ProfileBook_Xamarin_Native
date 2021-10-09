using MvvmCross.Binding.BindingContext;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.SignIn;
using ProfileBook_Native.Core.ViewModels.SignUp;
using UIKit;

namespace ProfileBook_Native.iOS.Views.SignUp
{
    public partial class SignUpView : BaseViewController<SignUpViewModel>
    {
        #region -- Overrides --

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetLocalizableStrings();
            SetSignUpButtonStyle();
            SetBindings();
        }

        #endregion

        #region -- Private helpers --

        private void SetBindings()
        {
            this.CreateBinding(LoginTextField).To<SignUpViewModel>(vm => vm.Login).Apply();
            this.CreateBinding(PasswordTextField).To<SignUpViewModel>(vm => vm.Password).Apply();
            this.CreateBinding(ConfirmPasswordTextField).To<SignUpViewModel>(vm => vm.ConfirmPassword).Apply();
            this.CreateBinding(SignUpButton).To<SignUpViewModel>(vm => vm.SignUpButtonTappedCommand).Apply();
        }

        public override void SetLocalizableStrings()
        {
            base.SetLocalizableStrings();
            Title = Strings.SignUpPage;
            LoginTextField.Placeholder = Strings.Login;
            PasswordTextField.Placeholder = Strings.Password;
            ConfirmPasswordTextField.Placeholder = Strings.ConfirmPassword;
            SignUpButton.SetTitle(Strings.SignUp.ToUpper(), UIControlState.Normal);
        }

        private void SetSignUpButtonStyle()
        {
            SignUpButton.Layer.BorderColor = UIColor.Black.CGColor;
            SignUpButton.Layer.BorderWidth = 1;
            SignUpButton.SetTitleColor(UIColor.FromRGBA(0, 0, 0, 127), UIControlState.Disabled);
        }

        #endregion
    }
}


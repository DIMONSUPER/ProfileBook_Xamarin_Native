using Foundation;
using MvvmCross.Binding.BindingContext;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.SignIn;
using UIKit;

namespace ProfileBook_Native.iOS.Views.SignIn
{
    public partial class SignInView : BaseViewController<SignInViewModel>
    {
        #region -- Overrides --

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetLocalizableStrings();
            SetSignInButtonStyle();
            SetBindings();
        }

        #endregion

        #region -- Private helpers --

        private void SetBindings()
        {
            this.CreateBinding(LoginTextField).To<SignInViewModel>(vm => vm.Login).Apply();
            this.CreateBinding(PasswordTextField).To<SignInViewModel>(vm => vm.Password).Apply();
            this.CreateBinding(SignInButton).To<SignInViewModel>(vm => vm.SignInCommand).Apply();
            this.CreateBinding(SignUpButton).To<SignInViewModel>(vm => vm.SignUpCommand).Apply();
        }

        private void SetLocalizableStrings()
        {
            Title = Strings.SignInPage;
            LoginTextField.Placeholder = Strings.Login;
            PasswordTextField.Placeholder = Strings.Password;
            SignInButton.SetTitle(Strings.SignIn.ToUpper(), UIControlState.Normal);
            SignUpButton.SetAttributedTitle(new(Strings.SignUp.ToUpper(), underlineStyle: NSUnderlineStyle.Single), UIControlState.Normal);
        }

        private void SetSignInButtonStyle()
        {
            SignInButton.Layer.BorderColor = UIColor.Black.CGColor;
            SignInButton.Layer.BorderWidth = 1;
            SignInButton.SetTitleColor(UIColor.FromRGBA(0, 0, 0, 127), UIControlState.Disabled);
        }

        #endregion
    }
}


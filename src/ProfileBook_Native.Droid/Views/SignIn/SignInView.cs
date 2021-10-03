using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.SignIn;

namespace ProfileBook_Native.Droid.Views.SignIn
{
    [MvxActivityPresentation]
    [Activity(Label = "Profile book", MainLauncher = true)]
    public class SignInView : BaseActivity<SignInViewModel>
    {
        private Button _signUpButton;
        private Button _signInButton;
        private EditText _passwordEditText;
        private EditText _loginEditText;
        private TextView _titleTextView;
        private TextView _rememberMeTextView;
        private Switch _rememberMeSwitch;

        #region -- Overrides --

        protected override int ActivityLayoutId => Resource.Layout.SignInView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _signUpButton = FindViewById(Resource.Id.sign_up_button) as Button;
            _signInButton = FindViewById(Resource.Id.sign_in_button) as Button;
            _passwordEditText = FindViewById(Resource.Id.password_edit_text) as EditText;
            _loginEditText = FindViewById(Resource.Id.login_edit_text) as EditText;
            _titleTextView = FindViewById(Resource.Id.textview_toolbar_title) as TextView;
            _rememberMeTextView = FindViewById(Resource.Id.remember_me_text_view) as TextView;
            _rememberMeSwitch = FindViewById(Resource.Id.remember_me_switch) as Switch;

            SetLocalazableStrings();

            _signUpButton.SetLayerPaint(new(PaintFlags.UnderlineText));
        }

        #endregion

        #region -- Private helpers --

        private void SetLocalazableStrings()
        {
            _loginEditText.Hint = Strings.Login;
            _passwordEditText.Hint = Strings.Password;
            _signUpButton.Text = Strings.SignUp;
            _signInButton.Text = Strings.SignIn;
            _titleTextView.Text = Strings.SignInPage;
            _rememberMeTextView.Text = Strings.RememberMe;
        }

        #endregion
    }
}

using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Text;
using Android.Text.Style;
using Android.Widget;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.SignIn;

namespace ProfileBook_Native.Droid.Views.SignIn
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SignInView : BaseActivity<SignInViewModel>
    {
        private Button _signUpButton;
        private Button _signInButton;
        private EditText _passwordEditText;
        private EditText _loginEditText;
        private TextView _rememberMeTextView;

        #region -- Overrides --

        protected override int ActivityLayoutId => Resource.Layout.SignInView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _signUpButton = FindViewById(Resource.Id.sign_up_button) as Button;
            _signInButton = FindViewById(Resource.Id.sign_in_button) as Button;
            _passwordEditText = FindViewById(Resource.Id.password_edit_text) as EditText;
            _loginEditText = FindViewById(Resource.Id.login_edit_text) as EditText;
            _rememberMeTextView = FindViewById(Resource.Id.remember_me_text_view) as TextView;

            SetLocalazableStrings();
        }

        #endregion

        #region -- Private helpers --

        private void SetLocalazableStrings()
        {
            _loginEditText.Hint = Strings.Login;
            _passwordEditText.Hint = Strings.Password;
            _signInButton.Text = Strings.SignIn;
            SupportActionBar.Title = Strings.SignInPage;
            _rememberMeTextView.Text = Strings.RememberMe;

            var content = new SpannableString(Strings.SignUp);
            content.SetSpan(new UnderlineSpan(), 0, content.Count(), 0);
            _signUpButton.SetText(content, TextView.BufferType.Spannable);
        }

        #endregion
    }
}

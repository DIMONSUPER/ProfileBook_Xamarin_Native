using System.Linq;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Text.Style;
using Android.Widget;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.SignIn;

namespace ProfileBook_Native.Droid.Views.SignIn
{
    [Activity]
    public class SignInView : BaseActivity<SignInViewModel>
    {
        private Button _signUpButton;
        private Button _signInButton;
        private EditText _passwordEditText;
        private EditText _loginEditText;
        private TextView _rememberMeTextView;

        #region -- Overrides --

        protected override int ActivityLayoutId => Resource.Layout.SignInView;

        protected override void OnStart()
        {
            base.OnStart();

            OverridePendingTransition(Resource.Animation.slide_in_left, Resource.Animation.slide_out_right);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _signUpButton = FindViewById<Button>(Resource.Id.sign_up_button);
            _signInButton = FindViewById<Button>(Resource.Id.sign_in_button);
            _passwordEditText = FindViewById<EditText>(Resource.Id.password_edit_text);
            _loginEditText = FindViewById<EditText>(Resource.Id.login_edit_text);
            _rememberMeTextView = FindViewById<TextView>(Resource.Id.remember_me_text_view);

            SetLocalazableStrings();
        }

        public override void OnBackPressed()
        {
            //It prevents user from exiting the app when back button pressed
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

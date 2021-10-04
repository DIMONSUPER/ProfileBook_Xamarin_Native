using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.SignUp;

namespace ProfileBook_Native.Droid.Views.SignUp
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class SignUpView : BaseActivity<SignUpViewModel>
    {
        private Button _signUpButton;
        private EditText _passwordEditText;
        private EditText _confirmPasswordEditText;
        private EditText _loginEditText;

        #region -- Overrides --

        protected override int ActivityLayoutId => Resource.Layout.SignUpView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _signUpButton = FindViewById(Resource.Id.sign_up_button) as Button;
            _passwordEditText = FindViewById(Resource.Id.password_edit_text) as EditText;
            _confirmPasswordEditText = FindViewById(Resource.Id.confirm_password_edit_text) as EditText;
            _loginEditText = FindViewById(Resource.Id.login_edit_text) as EditText;

            SetLocalazableStrings();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        #endregion

        #region -- Private helpers --

        private void SetLocalazableStrings()
        {
            _confirmPasswordEditText.Hint = Strings.ConfirmPassword;
            _signUpButton.Text = Strings.SignUp;
            _loginEditText.Hint = Strings.Login;
            _passwordEditText.Hint = Strings.Password;
            SupportActionBar.Title = Strings.SignUpPage;
        }

        #endregion
    }
}

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.SignUp;

namespace ProfileBook_Native.Droid.Views.SignUp
{
    [Activity]
    public class SignUpView : BaseActivity<SignUpViewModel>
    {
        private Button _signUpButton;
        private EditText _passwordEditText;
        private EditText _confirmPasswordEditText;
        private EditText _loginEditText;

        #region -- Overrides --

        protected override int ActivityLayoutId => Resource.Layout.SignUpView;

        protected override void OnStart()
        {
            base.OnStart();

            OverridePendingTransition(Resource.Animation.slide_in_right, Resource.Animation.slide_out_left);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _signUpButton = FindViewById<Button>(Resource.Id.sign_up_button);
            _passwordEditText = FindViewById<EditText>(Resource.Id.password_edit_text);
            _confirmPasswordEditText = FindViewById<EditText>(Resource.Id.confirm_password_edit_text);
            _loginEditText = FindViewById<EditText>(Resource.Id.login_edit_text);

            SetLocalazableStrings();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
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

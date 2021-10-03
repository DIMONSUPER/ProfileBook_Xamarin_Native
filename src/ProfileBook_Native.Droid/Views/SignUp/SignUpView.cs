using Android.OS;
using Android.Views;
using ProfileBook_Native.Core.ViewModels.SignUp;

namespace ProfileBook_Native.Droid.Views.SignUp
{
    public class SignUpView : BaseFragment<SignUpViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.SignInView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}

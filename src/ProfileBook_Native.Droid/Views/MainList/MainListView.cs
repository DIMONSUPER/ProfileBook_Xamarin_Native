using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.MainList;

namespace ProfileBook_Native.Droid.Views.MainList
{
    [Activity]
    public class MainListView : BaseActivity<MainListViewModel>
    {
        private TextView _noProfilesTextView;

        #region -- Overrides --

        protected override int ActivityLayoutId => Resource.Layout.MainListView;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.settings)
            {
                if (ViewModel.SettingsButtonTappedCommand is not null && ViewModel.SettingsButtonTappedCommand.CanExecute(null))
                {
                    ViewModel.SettingsButtonTappedCommand.Execute(null);
                }
            }
            else if (item.ItemId == Resource.Id.log_out)
            {
                if (ViewModel.LogOutButtonTappedCommand is not null && ViewModel.LogOutButtonTappedCommand.CanExecute(null))
                {
                    ViewModel.LogOutButtonTappedCommand.Execute(null);
                }
            }

            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(Menu.None, Resource.Id.settings, 2, string.Empty)
                .SetIcon(Resource.Drawable.ic_settings)
                .SetShowAsActionFlags(ShowAsAction.IfRoom);

            menu.Add(Menu.None, Resource.Id.log_out, 1, string.Empty)
                .SetIcon(Resource.Drawable.ic_exit_to_app)
                .SetShowAsActionFlags(ShowAsAction.IfRoom);

            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _noProfilesTextView = FindViewById<TextView>(Resource.Id.no_profiles_text_view);
        }

        protected override void OnStart()
        {
            base.OnStart();

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
            SupportActionBar.Title = Strings.MainList;
            _noProfilesTextView.Text = Strings.NoProfiles;
        }

        #endregion
    }
}

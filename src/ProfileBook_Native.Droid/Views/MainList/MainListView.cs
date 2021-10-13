using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Extensions;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.MainList;
using static Android.Widget.AdapterView;

namespace ProfileBook_Native.Droid.Views.MainList
{
    [Activity]
    public class MainListView : BaseActivity<MainListViewModel>
    {
        private ProfileListView _profileListView;
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

        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.delete)
            {
                var acmi = item.MenuInfo as AdapterContextMenuInfo;

                var selectedProfile = _profileListView.ItemsSource.ElementAt(acmi.Position);

                if (ViewModel.DeleteButtonTappedCommand is not null && ViewModel.DeleteButtonTappedCommand.CanExecute(selectedProfile))
                {
                    ViewModel.DeleteButtonTappedCommand.Execute(selectedProfile);
                }
            }

            return base.OnContextItemSelected(item);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            if (v.Id == Resource.Id.profile_list_view)
            {
                menu.Add(Menu.None, Resource.Id.delete, 1, Strings.Delete);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _profileListView = FindViewById<ProfileListView>(Resource.Id.profile_list_view);
            _noProfilesTextView = FindViewById<TextView>(Resource.Id.no_profiles_text_view);

            RegisterForContextMenu(_profileListView);
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

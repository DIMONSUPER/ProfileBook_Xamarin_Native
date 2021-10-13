using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.AddEditProfile;
using ProfileBook_Native.Droid.Converters;

namespace ProfileBook_Native.Droid.Views.AddEditProfile
{
    [Activity]
    public class AddEditProfileView : BaseActivity<AddEditProfileViewModel>
    {
        private ImageButton _profileImageButton;
        private EditText _profileNickNameEditText;
        private EditText _profileNameEditText;
        private EditText _profileDescriptionEditText;
        private IMenuItem _saveButton;

        public AddEditProfileView()
        {
        }

        #region -- Public properties --

        private bool _isSaveButtonEnabled;
        public bool IsSaveButtonEnabled
        {
            get => _isSaveButtonEnabled;
            set
            {
                _saveButton?.SetEnabled(value);
                _isSaveButtonEnabled = value;
            }
        }

        #endregion

        #region -- Overrides --

        protected override int ActivityLayoutId => Resource.Layout.AddEditProfileView;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
            }
            else if (item.ItemId == Resource.Id.save)
            {
                if (ViewModel.SaveButtonTappedCommand is not null && ViewModel.SaveButtonTappedCommand.CanExecute(null))
                {
                    ViewModel.SaveButtonTappedCommand.Execute(null);
                }
            }

            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            _saveButton = menu.Add(Menu.None, Resource.Id.save, 1, string.Empty)
                              .SetIcon(Resource.Drawable.ic_save)
                              .SetShowAsActionFlags(ShowAsAction.IfRoom);

            this.CreateBinding().For(x => x.IsSaveButtonEnabled).To<AddEditProfileViewModel>(x => x.CanExecute).Apply();

            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _profileImageButton = FindViewById<ImageButton>(Resource.Id.prfile_image_button);
            _profileNickNameEditText = FindViewById<EditText>(Resource.Id.profile_nickname_edit_text);
            _profileNameEditText = FindViewById<EditText>(Resource.Id.profile_name_edit_text);
            _profileDescriptionEditText = FindViewById<EditText>(Resource.Id.profile_description_edit_text);

            var set = this.CreateBindingSet<AddEditProfileView, AddEditProfileViewModel>();
            set.Bind(SupportActionBar).For(x => x.Title).To(vm => vm.Title);
            set.Bind(_profileImageButton).For(x => x.Drawable).To(vm => vm.CurrentProfile.ProfileImage).WithConversion(new BytesToDrawableConverter(), ApplicationContext);
            set.Apply();

            SetLocalazableStrings();
        }

        #endregion

        #region -- Private helpers --

        private void SetLocalazableStrings()
        {
            _profileNickNameEditText.Hint = Strings.NickName;
            _profileNameEditText.Hint = Strings.Name;
            _profileDescriptionEditText.Hint = Strings.Description;
        }

        #endregion
    }
}

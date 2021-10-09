using System.IO;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.Views;
using ProfileBook_Native.Core.Models;

namespace ProfileBook_Native.Droid.Views.MainList
{
    public class ProfileListViewItem : MvxListItemView
    {
        private readonly ImageView _profileImageView;
        private readonly TextView _profileNameTextView;
        private readonly TextView _profileNicknameTextView;

        public ProfileListViewItem(
            Context context,
            IMvxLayoutInflaterHolder layoutInflaterHolder,
            object dataContext,
            ViewGroup parent,
            int templateId)
            : base(context, layoutInflaterHolder, dataContext, parent, templateId)
        {
            _profileImageView = Content.FindViewById<ImageView>(Resource.Id.profile_image_view);
            _profileNameTextView = Content.FindViewById<TextView>(Resource.Id.profile_name_text_view);
            _profileNicknameTextView = Content.FindViewById<TextView>(Resource.Id.profile_nickname_text_view);

            var set = this.CreateBindingSet<ProfileListViewItem, ProfileBindableModel>();
            set.Bind(ProfileImage).To(vm => vm.ProfileImage);
            set.Bind(_profileNameTextView).To(vm => vm.Name);
            set.Bind(_profileNicknameTextView).To(vm => vm.NickName);
            set.Apply();
        }

        #region -- Public properties --

        private byte[] _profileImage;
        public byte[] ProfileImage
        {
            get => _profileImage;
            set
            {
                if (_profileImage != value)
                {
                    SetProfileImageDrawable(value);

                    _profileImage = value;
                }
            }
        }

        #endregion

        #region -- Private helpers --

        private void SetProfileImageDrawable(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                using var stream = new MemoryStream(bytes);
                var drawable = Drawable.CreateFromStream(stream, null);
                _profileImageView.SetImageDrawable(drawable);
            }
        }

        #endregion
    }
}

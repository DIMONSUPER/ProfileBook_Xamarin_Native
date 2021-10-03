using System;
using System.Windows.Input;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.iOS.Converters;
using UIKit;

namespace ProfileBook_Native.iOS.Views.MainList
{
    public partial class ProfileViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new(nameof(ProfileViewCell));
        public static readonly UINib Nib;

        static ProfileViewCell()
        {
            Nib = UINib.FromName(nameof(ProfileViewCell), NSBundle.MainBundle);
        }

        protected ProfileViewCell(IntPtr handle) : base(handle)
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            this.DelayBind(() =>
            {
                SetBindings();
            });
        }

        #region -- Public properties --

        public ICommand SelectedCommand { get; set; }

        #endregion

        #region -- Overrides --

        public override void SetSelected(bool selected, bool animated)
        {
            if (selected)
            {
                var profile = DataContext as ProfileBindableModel;
                if (SelectedCommand != null && SelectedCommand.CanExecute(profile))
                {
                    SelectedCommand.Execute(profile);
                }
            }
        }

        #endregion

        #region -- Private helpers --

        private void SetBindings()
        {
            var set = this.CreateBindingSet<ProfileViewCell, ProfileBindableModel>();

            set.Bind(ProfileImageView).To(vm => vm.ProfileImage).WithConversion(new BytesToImageConverter());
            set.Bind().For(x => x.SelectedCommand).To(vm => vm.TapCommad);
            set.Bind(NameLabel).To(vm => vm.Name);
            set.Bind(NicknameLabel).To(vm => vm.NickName);
            set.Bind(DateLabel).To(vm => vm.Date);

            set.Apply();
        }

        #endregion
    }
}


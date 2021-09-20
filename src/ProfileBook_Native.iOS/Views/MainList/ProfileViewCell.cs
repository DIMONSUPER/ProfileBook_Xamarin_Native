using System;
using System.Globalization;
using System.Windows.Input;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Converters;
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
            this.CreateBinding(ImageView)
            .For(x => x.Image)
            .To<ProfileBindableModel>(vm => vm.ProfileImage)
            .WithConversion(new StringToImageConverter(), null)
            .Apply();

            this.CreateBinding().For(x => x.SelectedCommand).To<ProfileBindableModel>(vm => vm.TapCommad).Apply();
            this.CreateBinding(NameLabel).To<ProfileBindableModel>(vm => vm.Name).Apply();
            this.CreateBinding(NicknameLabel).To<ProfileBindableModel>(vm => vm.NickName).Apply();
            this.CreateBinding(DateLabel).To<ProfileBindableModel>(vm => vm.Date).Apply();
        }

        #endregion
    }
}


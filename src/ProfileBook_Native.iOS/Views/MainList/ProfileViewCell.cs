using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using ProfileBook_Native.Core.Models;
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
                ImageView.Image = UIImage.FromFile("ic_camera_alt.png");
                //this.CreateBinding(ImageView).For(x => x.Image).To<ProfileBindableModel>(vm => UIImage.FromFile(vm.ProfileImage));
                this.CreateBinding(NameLabel).To<ProfileBindableModel>(vm => vm.Name).Apply();
                this.CreateBinding(NicknameLabel).To<ProfileBindableModel>(vm => vm.NickName).Apply();
                this.CreateBinding(DateLabel).To<ProfileBindableModel>(vm => vm.Date).Apply();
            });
        }
    }
}


using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using ProfileBook_Native.Core.Models;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.AddEditProfile;
using ProfileBook_Native.iOS.Converters;
using UIKit;

namespace ProfileBook_Native.iOS.Views.AddEditProfile
{
    public partial class AddEditProfileView : MvxViewController<AddEditProfileViewModel>
    {
        public AddEditProfileView() : base(nameof(AddEditProfileView), null)
        {
        }

        #region -- Overrides --

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CreateToolBar();
            SetStyles();
            SetTranslations();
            SetBindings();
        }

        #endregion

        #region -- Private helpers --

        private void SetTranslations()
        {
            NicknameLabel.Placeholder = Strings.NickName;
            NameLabel.Placeholder = Strings.Name;
            //TODO: add placeholder for DescriptionTextView
        }

        private void SetBindings()
        {
            this.CreateBinding().For(x => x.Title).To<AddEditProfileViewModel>(vm => vm.Title).Apply();

            this.CreateBinding(ProfileImageView)
            .For(x => x.Image)
            .To<AddEditProfileViewModel>(vm => vm.CurrentProfile.ProfileImage)
            .WithConversion(new StringToImageConverter(), new CGSize(135, 150))
            .Apply();

            this.CreateBinding(NicknameLabel).To<AddEditProfileViewModel>(vm => vm.CurrentProfile.NickName).Apply();
            this.CreateBinding(NameLabel).To<AddEditProfileViewModel>(vm => vm.CurrentProfile.Name).Apply();
            this.CreateBinding(DescriptionTextView).To<AddEditProfileViewModel>(vm => vm.CurrentProfile.Description).Apply();
        }

        private void CreateToolBar()
        {
            var saveButton = new UIButton() { Frame = new(0, 0, 25, 25) };
            saveButton.SetBackgroundImage(UIImage.FromFile("ic_save.png"), UIControlState.Normal);
            this.CreateBinding(saveButton).To<AddEditProfileViewModel>(vm => vm.SaveButtonTappedCommand).Apply();

            var saveBarButton = new UIBarButtonItem() { CustomView = saveButton };

            this.CreateBinding(saveBarButton).For(x => x.Enabled).To<AddEditProfileViewModel>(vm => vm.CanExecute).Apply();

            NavigationItem.SetRightBarButtonItem(saveBarButton, false);
        }

        private void SetStyles()
        {
            DescriptionTextView.Layer.BorderWidth = 1;
            DescriptionTextView.Layer.BorderColor = UIColor.Black.CGColor;
            DescriptionTextView.Layer.CornerRadius = 6;

            NicknameLabel.Layer.BorderWidth = 1;
            NicknameLabel.Layer.BorderColor = UIColor.Black.CGColor;
            NicknameLabel.Layer.CornerRadius = 6;

            NameLabel.Layer.BorderWidth = 1;
            NameLabel.Layer.BorderColor = UIColor.Black.CGColor;
            NameLabel.Layer.CornerRadius = 6;
        }

        #endregion
    }
}


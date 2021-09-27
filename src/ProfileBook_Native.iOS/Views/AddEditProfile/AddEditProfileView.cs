using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.AddEditProfile;
using ProfileBook_Native.iOS.Converters;
using UIKit;

namespace ProfileBook_Native.iOS.Views.AddEditProfile
{
    public partial class AddEditProfileView : BaseViewController<AddEditProfileViewModel>
    {
        #region -- Overrides --

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CreateToolBar();
            SetStyles();
            SetTranslations();
            CreateObserver();
            SetBindings();
        }

        #endregion

        #region -- Private helpers --

        private void CreateObserver()
        {
            var observer = NSNotificationCenter.DefaultCenter.AddObserver(UITextView.TextDidChangeNotification,
                notification => DescriptionPlaceholderLabel.Hidden = !string.IsNullOrEmpty(DescriptionTextView.Text));
        }

        private void SetTranslations()
        {
            NicknameLabel.Placeholder = Strings.NickName;
            NameLabel.Placeholder = Strings.Name;
            DescriptionPlaceholderLabel.Text = Strings.Description;
        }

        public UIImage ProfileImage
        {
            get => ProfileImageButton.ImageView.Image;
            set => ProfileImageButton.SetImage(value, UIControlState.Normal);
        }

        private void SetBindings()
        {
            this.CreateBinding().For(x => x.Title).To<AddEditProfileViewModel>(vm => vm.Title).Apply();

            this.CreateBinding()
            .For(x => x.ProfileImage)
            .To<AddEditProfileViewModel>(vm => vm.CurrentProfile.ProfileImage)
            .WithConversion(new StringToImageConverter(), new CGSize(135, 150))
            .Apply();

            this.CreateBinding(ProfileImageButton).To<AddEditProfileViewModel>(vm => vm.ProfileImageTappedCommand).Apply();

            this.CreateBinding(NicknameLabel).To<AddEditProfileViewModel>(vm => vm.CurrentProfile.NickName).Apply();
            this.CreateBinding(NameLabel).To<AddEditProfileViewModel>(vm => vm.CurrentProfile.Name).Apply();
            this.CreateBinding(DescriptionTextView).To<AddEditProfileViewModel>(vm => vm.CurrentProfile.Description).Apply();
        }

        private void CreateToolBar()
        {
            var saveButton = new UIButton();
            saveButton.SetBackgroundImage(GetResizedImage(UIImage.FromFile("ic_save.png"), new(25, 25)), UIControlState.Normal);
            this.CreateBinding(saveButton).To<AddEditProfileViewModel>(vm => vm.SaveButtonTappedCommand).Apply();

            var saveBarButton = new UIBarButtonItem(saveButton);
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

        private UIImage GetResizedImage(UIImage image, CGSize size)
        {
            UIGraphics.BeginImageContext(size);
            image.Draw(new CGRect(0, 0, size.Width, size.Height));

            var newImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return newImage;
        }

        #endregion
    }
}


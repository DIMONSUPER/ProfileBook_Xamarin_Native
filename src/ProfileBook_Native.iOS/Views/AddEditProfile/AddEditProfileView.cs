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
        private readonly NSObject _observer;

        public AddEditProfileView()
        {
            _observer = UITextView.Notifications.ObserveTextDidChange((sender, args) =>
            DescriptionPlaceholderLabel.Hidden = !string.IsNullOrEmpty(DescriptionTextView.Text));
        }

        #region -- Public properties --

        public UIImage ProfileImage
        {
            get => ProfileImageButton.ImageView.Image;
            set => ProfileImageButton.SetImage(value, UIControlState.Normal);
        }

        #endregion

        #region -- Overrides --

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _observer.Dispose();
            }

            base.Dispose(disposing);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetStyles();
            CreateToolBar();
        }

        public override void SetLocalizableStrings()
        {
            base.SetLocalizableStrings();

            NicknameLabel.Placeholder = Strings.NickName;
            NameLabel.Placeholder = Strings.Name;
            DescriptionPlaceholderLabel.Text = Strings.Description;
        }

        protected override void BindView()
        {
            base.BindView();

            var set = this.CreateBindingSet<AddEditProfileView, AddEditProfileViewModel>();

            set.Bind().For(x => x.ProfileImage).To(vm => vm.CurrentProfile.ProfileImage).WithConversion(new BytesToImageConverter());
            set.Bind().For(x => x.Title).To(vm => vm.Title);
            set.Bind(ProfileImageButton).To(vm => vm.ProfileImageTappedCommand);
            set.Bind(NicknameLabel).To(vm => vm.CurrentProfile.NickName);
            set.Bind(NameLabel).To(vm => vm.CurrentProfile.Name);
            set.Bind(DescriptionTextView).To(vm => vm.CurrentProfile.Description);

            set.Apply();

            DescriptionPlaceholderLabel.Hidden = !string.IsNullOrEmpty(DescriptionTextView.Text);
        }

        #endregion

        #region -- Private helpers --

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


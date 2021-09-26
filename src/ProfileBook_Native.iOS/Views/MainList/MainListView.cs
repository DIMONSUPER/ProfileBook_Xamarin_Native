using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.MainList;
using UIKit;

namespace ProfileBook_Native.iOS.Views.MainList
{
    public partial class MainListView : BaseViewController<MainListViewModel>
    {
        #region -- Overrides --

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.HidesBackButton = true;
            CreateToolBar();
            SetAddButtonStyle();
            SetLocalizableStrings();
            SetBindings();
        }

        #endregion

        #region -- Private helpers --

        private void SetBindings()
        {
            this.CreateBinding(ProfilesEmptyLabel).For(x => x.Hidden).To<MainListViewModel>(vm => vm.HasProfiles).Apply();
            this.CreateBinding(AddButton).To<MainListViewModel>(vm => vm.AddButtonTappedCommand).Apply();
            
            BindCollectionView();
        }

        private void SetAddButtonStyle()
        {
            AddButton.Layer.BorderColor = UIColor.Black.CGColor;
            AddButton.Layer.BorderWidth = 1;
        }

        private void CreateToolBar()
        {
            var settingsButton = new UIButton(new CGRect(15, 0, 25, 25));
            settingsButton.SetBackgroundImage(UIImage.FromFile("ic_settings.png"), UIControlState.Normal);
            this.CreateBinding(settingsButton).To<MainListViewModel>(vm => vm.SettingsButtonTappedCommand).Apply();

            var exitButton = new UIButton(new CGRect(0, 0, 25, 25));
            exitButton.SetBackgroundImage(UIImage.FromFile("ic_exit_to_app.png"), UIControlState.Normal);
            this.CreateBinding(exitButton).To<MainListViewModel>(vm => vm.LogOutButtonTappedCommand).Apply();

            var settingsButtonView = new UIView(new CGRect(0, 0, 40, 25));
            var exitButtonView = new UIView(new CGRect(0, 0, 25, 25));

            settingsButtonView.AddSubview(settingsButton);
            exitButtonView.AddSubview(exitButton);

            var items = new UIBarButtonItem[]
            {
                new(settingsButtonView),
                new(exitButtonView),
            };

            NavigationItem.SetRightBarButtonItems(items, false);
        }

        private void SetLocalizableStrings()
        {
            Title = Strings.MainList;
            ProfilesEmptyLabel.Text = Strings.NoProfiles;
        }

        private void BindCollectionView()
        {
            ProfilesTableView.SeparatorColor = UIColor.Clear;
            ProfilesTableView.RegisterNibForCellReuse(ProfileViewCell.Nib, ProfileViewCell.Key);
            var source = new ProfileTableViewSource(ProfilesTableView);
            ProfilesTableView.Source = source;
            this.CreateBinding(source).To<MainListViewModel>(vm => vm.Profiles).Apply();
        }

        #endregion
    }
}


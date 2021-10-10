using Cirrious.FluentLayouts.Touch;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.iOS.Styles;
using UIKit;

namespace ProfileBook_Native.iOS.Views
{
    public abstract class BaseViewController<TViewModel> : MvxViewController<TViewModel>, ILocalazibleViewController
        where TViewModel : class, IMvxViewModel
    {
        private UITapGestureRecognizer _tapGesture;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            NavigationController.NavigationBar.Translucent = false;
            NavigationController.NavigationBar.Hidden = false;
            NavigationController.NavigationBar.BarTintColor = ColorPalette.Primary;
            NavigationController.NavigationBar.TintColor = UIColor.White;
            NavigationController.NavigationBar.TitleTextAttributes = new() { ForegroundColor = UIColor.White };

            NavigationController.SetNeedsStatusBarAppearanceUpdate();

            CreateView();

            LayoutView();

            BindView();

            SetLocalizableStrings();

            _tapGesture = new UITapGestureRecognizer(() => View.EndEditing(true))
            {
                CancelsTouchesInView = false
            };

            View.AddGestureRecognizer(_tapGesture);
        }

        #region -- Overrides --

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                View.RemoveGestureRecognizer(_tapGesture);
            }

            base.Dispose(disposing);
        }

        #endregion

        #region -- Virtual helpers --

        public virtual void SetLocalizableStrings()
        {
            if (NavigationController?.NavigationBar?.TopItem?.BackBarButtonItem is not null)
            {
                NavigationController.NavigationBar.TopItem.BackBarButtonItem.Title = Strings.Back;
            }
        }

        protected virtual void CreateView()
        {
        }

        protected virtual void LayoutView()
        {
        }

        protected virtual void BindView()
        {
        }

        #endregion
    }
}

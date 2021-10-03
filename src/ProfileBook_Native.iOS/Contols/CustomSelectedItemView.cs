using System;
using CoreGraphics;
using ProfileBook_Native.iOS.Styles;
using UIKit;

namespace ProfileBook_Native.iOS.Contols
{
    public class CustomSelectedItemView : UIView
    {
        private UITapGestureRecognizer _tapGestureRecognizer;

        public CustomSelectedItemView()
        {
            _tapGestureRecognizer = new UITapGestureRecognizer(OnViewTapped);

            Layer.CornerRadius = 6;
            Layer.BorderWidth = 1;

            Layer.BorderColor = TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Light
                        ? _isSelected ? ColorPalette.AccentDark.CGColor : ColorPalette.AccentLight.CGColor
                        : _isSelected ? ColorPalette.AccentLight.CGColor : ColorPalette.AccentDark.CGColor;

            AddGestureRecognizer(_tapGestureRecognizer);

            CreateTitleView();
        }

        #region -- Public properties --

        public event EventHandler Selected;

        public UILabel TitleLabel { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;

                    if (_isSelected)
                    {
                        Selected?.Invoke(this, new());
                    }

                    Layer.BorderColor = TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Light
                        ? _isSelected ? ColorPalette.AccentDark.CGColor : ColorPalette.AccentLight.CGColor
                        : _isSelected ? ColorPalette.AccentLight.CGColor : ColorPalette.AccentDark.CGColor;

                    Layer.BorderWidth = _isSelected ? 2 : 1;
                }
            }
        }

        #endregion

        #region -- Overrides --

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            Layer.BorderColor = TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Light
                        ? _isSelected ? ColorPalette.AccentDark.CGColor : ColorPalette.AccentLight.CGColor
                        : _isSelected ? ColorPalette.AccentLight.CGColor : ColorPalette.AccentDark.CGColor;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RemoveGestureRecognizer(_tapGestureRecognizer);
                _tapGestureRecognizer = null;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region -- Private helpers --

        private void CreateTitleView()
        {
            TitleLabel = new()
            {
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(14),
            };

            TitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            var leading = NSLayoutConstraint.Create(TitleLabel, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, this, NSLayoutAttribute.Leading, 1, 0);
            var trailing = NSLayoutConstraint.Create(TitleLabel, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, this, NSLayoutAttribute.Trailing, 1, 0);
            var bottom = NSLayoutConstraint.Create(TitleLabel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this, NSLayoutAttribute.Bottom, 1, 0);
            var top = NSLayoutConstraint.Create(TitleLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1, 0);

            AddSubview(TitleLabel);
            AddConstraints(new NSLayoutConstraint[] { leading, trailing, bottom, top });
        }

        private void OnViewTapped()
        {
            IsSelected = true;
        }

        #endregion
    }
}

using System;
using System.Globalization;
using CoreGraphics;
using MvvmCross.Converters;
using UIKit;

namespace ProfileBook_Native.iOS.Converters
{
    public class StringToImageConverter : MvxValueConverter<string, UIImage>
    {
        #region -- Overrides --

        protected override UIImage Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            var deafultValue = "pic_profile.png";

            if (parameter is string stringParameter)
            {
                deafultValue = stringParameter;
            }

            return !string.IsNullOrWhiteSpace(value)
                ? UIImage.FromFile(value)
                : UIImage.FromFile(deafultValue);
        }

        #endregion

        #region -- Private helpers --

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

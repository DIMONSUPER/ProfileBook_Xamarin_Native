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
            var defaultSize = new CGSize(90, 100);

            if (parameter is CGSize size)
            {
                defaultSize = size;
            }

            return !string.IsNullOrWhiteSpace(value)
                ? GetResizedImage(UIImage.FromFile(value), defaultSize)
                : GetResizedImage(UIImage.FromFile("pic_profile.png"), defaultSize);
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

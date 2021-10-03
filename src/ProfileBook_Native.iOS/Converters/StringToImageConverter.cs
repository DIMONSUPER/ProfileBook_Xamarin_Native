using System;
using System.Globalization;
using Foundation;
using MvvmCross.Converters;
using UIKit;

namespace ProfileBook_Native.iOS.Converters
{
    public class BytesToImageConverter : MvxValueConverter<byte[], UIImage>
    {
        #region -- Overrides --

        protected override UIImage Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            UIImage image = null;

            if (value is not null && value.Length > 0)
            {
                image = new UIImage(NSData.FromArray(value));
            }

            return image ?? UIImage.FromFile("pic_profile.png");
        }

        #endregion
    }
}

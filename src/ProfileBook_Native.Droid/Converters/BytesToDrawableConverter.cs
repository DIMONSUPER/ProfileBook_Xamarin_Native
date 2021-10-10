using System;
using System.Globalization;
using System.IO;
using Android.Content;
using Android.Graphics.Drawables;
using MvvmCross.Converters;

namespace ProfileBook_Native.Droid.Converters
{
    public class BytesToDrawableConverter : MvxValueConverter<byte[], Drawable>
    {
        #region -- Overrides --

        protected override Drawable Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            Drawable drawable = null;
            var context = parameter as Context;

            if (value is not null && value.Length > 0)
            {
                using var stream = new MemoryStream(value);
                drawable = Drawable.CreateFromStream(stream, null);
            }

            return drawable ?? context?.GetDrawable(Resource.Drawable.pic_profile);
        }

        #endregion
    }
}

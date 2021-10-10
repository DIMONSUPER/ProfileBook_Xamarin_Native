using System;
using System.Globalization;
using MvvmCross.Converters;

namespace ProfileBook_Native.Core.Converters
{
    public class InverseBoolConverter : MvxValueConverter<bool, bool>
    {
        #region -- Overrides --

        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value;
        }

        protected override bool ConvertBack(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value;
        }

        #endregion
    }
}

using System;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace ProfileBook_Native.iOS.Views.Settings
{
    public class LanguagePickerDataSource : MvxPickerViewModel
    {
        public LanguagePickerDataSource(UIPickerView pickerView) : base(pickerView)
        {
            pickerView.Delegate = this;
        }

        #region -- Overrides --

        public override nint GetRowsInComponent(UIPickerView picker, nint component) => ItemsSource.Count();

        public override nint GetComponentCount(UIPickerView picker) => 1;

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            var res = ItemsSource.ElementAt((int)row).ToString();

            return res;
        }

        #endregion
    }
}

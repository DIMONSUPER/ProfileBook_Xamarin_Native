// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ProfileBook_Native.iOS.Views.Settings
{
	[Register ("SettingsView")]
	partial class SettingsView
	{
		[Outlet]
		UIKit.UIPickerView LanguagePickerView { get; set; }

		[Outlet]
		UIKit.UILabel SortByLabel { get; set; }

		[Outlet]
		UIKit.UIStackView SortOptionsStackView { get; set; }

		[Outlet]
		UIKit.UILabel ThemeLabel { get; set; }

		[Outlet]
		UIKit.UIStackView ThemeOptionsStackView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SortByLabel != null) {
				SortByLabel.Dispose ();
				SortByLabel = null;
			}

			if (SortOptionsStackView != null) {
				SortOptionsStackView.Dispose ();
				SortOptionsStackView = null;
			}

			if (ThemeLabel != null) {
				ThemeLabel.Dispose ();
				ThemeLabel = null;
			}

			if (ThemeOptionsStackView != null) {
				ThemeOptionsStackView.Dispose ();
				ThemeOptionsStackView = null;
			}

			if (LanguagePickerView != null) {
				LanguagePickerView.Dispose ();
				LanguagePickerView = null;
			}
		}
	}
}

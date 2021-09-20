// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ProfileBook_Native.iOS.Views.AddEditProfile
{
	[Register ("AddEditProfileView")]
	partial class AddEditProfileView
	{
		[Outlet]
		UIKit.UITextView DescriptionTextView { get; set; }

		[Outlet]
		UIKit.UITextField NameLabel { get; set; }

		[Outlet]
		UIKit.UITextField NicknameLabel { get; set; }

		[Outlet]
		UIKit.UIImageView ProfileImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProfileImageView != null) {
				ProfileImageView.Dispose ();
				ProfileImageView = null;
			}

			if (NicknameLabel != null) {
				NicknameLabel.Dispose ();
				NicknameLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (DescriptionTextView != null) {
				DescriptionTextView.Dispose ();
				DescriptionTextView = null;
			}
		}
	}
}

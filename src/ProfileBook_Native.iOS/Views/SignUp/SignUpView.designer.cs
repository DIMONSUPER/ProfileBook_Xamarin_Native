// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;

namespace ProfileBook_Native.iOS.Views.SignUp
{
    [Register (nameof(SignUpView))]
	partial class SignUpView
	{
		[Outlet]
		UIKit.UITextField ConfirmPasswordTextField { get; set; }

		[Outlet]
		UIKit.UITextField LoginTextField { get; set; }

		[Outlet]
		UIKit.UITextField PasswordTextField { get; set; }

		[Outlet]
		UIKit.UIButton SignUpButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LoginTextField != null) {
				LoginTextField.Dispose ();
				LoginTextField = null;
			}

			if (PasswordTextField != null) {
				PasswordTextField.Dispose ();
				PasswordTextField = null;
			}

			if (ConfirmPasswordTextField != null) {
				ConfirmPasswordTextField.Dispose ();
				ConfirmPasswordTextField = null;
			}

			if (SignUpButton != null) {
				SignUpButton.Dispose ();
				SignUpButton = null;
			}
		}
	}
}

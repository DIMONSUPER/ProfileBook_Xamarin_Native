// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ProfileBook_Native.iOS.Views.SignIn
{
	[Register ("SignInView")]
	partial class SignInView
	{
		[Outlet]
		UIKit.UITextField LoginTextField { get; set; }

		[Outlet]
		UIKit.UITextField PasswordTextField { get; set; }

		[Outlet]
		UIKit.UIButton SignInButton { get; set; }

		[Outlet]
		UIKit.UIButton SignUpButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PasswordTextField != null) {
				PasswordTextField.Dispose ();
				PasswordTextField = null;
			}

			if (LoginTextField != null) {
				LoginTextField.Dispose ();
				LoginTextField = null;
			}

			if (SignInButton != null) {
				SignInButton.Dispose ();
				SignInButton = null;
			}

			if (SignUpButton != null) {
				SignUpButton.Dispose ();
				SignUpButton = null;
			}
		}
	}
}

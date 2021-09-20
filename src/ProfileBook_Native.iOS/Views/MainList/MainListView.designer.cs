// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ProfileBook_Native.iOS.Views.MainList
{
	[Register ("MainListView")]
	partial class MainListView
	{
		[Outlet]
		UIKit.UIButton AddButton { get; set; }

		[Outlet]
		UIKit.UILabel ProfilesEmptyLabel { get; set; }

		[Outlet]
		UIKit.UITableView ProfilesTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProfilesEmptyLabel != null) {
				ProfilesEmptyLabel.Dispose ();
				ProfilesEmptyLabel = null;
			}

			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}

			if (ProfilesTableView != null) {
				ProfilesTableView.Dispose ();
				ProfilesTableView = null;
			}
		}
	}
}

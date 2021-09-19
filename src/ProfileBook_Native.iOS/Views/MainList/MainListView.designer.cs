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
		UIKit.UITableView ProfilesTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProfilesTableView != null) {
				ProfilesTableView.Dispose ();
				ProfilesTableView = null;
			}
		}
	}
}

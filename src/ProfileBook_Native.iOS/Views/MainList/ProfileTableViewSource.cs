using System;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace ProfileBook_Native.iOS.Views.MainList
{
    public class ProfileTableViewSource : MvxTableViewSource
    {
        public ProfileTableViewSource(UITableView tableView) : base(tableView)
        {
        }

        #region -- Overrides --

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return tableView.DequeueReusableCell(ProfileViewCell.Key, indexPath);
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 120;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        #endregion
    }
}

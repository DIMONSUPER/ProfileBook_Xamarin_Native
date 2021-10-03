using System;
using System.Collections.Generic;
using System.Windows.Input;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using ProfileBook_Native.Core.Models;
using UIKit;

namespace ProfileBook_Native.iOS.Views.MainList
{
    public class ProfileTableViewSource : MvxTableViewSource
    {
        public ProfileTableViewSource(UITableView tableView) : base(tableView)
        {
        }

        #region -- Public properties --

        public ICommand ItemDeletedCommand { get; set; }

        public IList<ProfileBindableModel> Items
        {
            get => ItemsSource as IList<ProfileBindableModel>;
            set => ItemsSource = value;
        }

        #endregion

        #region -- Overrides --

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                if (ItemDeletedCommand is not null && ItemDeletedCommand.CanExecute(Items[indexPath.Row]))
                {
                    ItemDeletedCommand?.Execute(Items[indexPath.Row]);
                }
            }
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using VirtualPiggyBank.Core.Model;
using VirtualPiggyBank.Core.Repo;

namespace VirtualPiggyBank.DataSource
{
    class QuickDepositTableDataSource : UITableViewSource
    {
        List<QuickDeposit> quickDepositTypes = new List<QuickDeposit>();

        SettingsViewController callingController;

        NSString cellIdentifier = new NSString("ComponentCell");

        public QuickDepositTableDataSource(SettingsViewController CallingController)
        {
            callingController = CallingController;

            var db = BankRepository.Connection();

            quickDepositTypes = db.CreateCommand("SELECT * FROM QuickDepositTypes").ExecuteQuery<QuickDeposit>();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value1, cellIdentifier);
            }

            QuickDeposit quickDeposit = quickDepositTypes[indexPath.Row];
            cell.TextLabel.Text = quickDeposit.QuickDepositType;
            cell.DetailTextLabel.Text = "$" + quickDeposit.Amount.ToString();

            return cell;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return quickDepositTypes.Count;
        }
    }
}
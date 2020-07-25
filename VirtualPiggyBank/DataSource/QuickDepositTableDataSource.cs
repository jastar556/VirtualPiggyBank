using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
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

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 40;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            // I just swapped Height | Width in following line
            UILabel lblHeader = new UILabel(new CGRect(1, 1, tableView.SectionHeaderHeight, tableView.Frame.Width));
            lblHeader.Font = UIFont.FromName("TimesNewRomanPS-BoldMT", 25);
            lblHeader.BackgroundColor = UIColor.SystemGray2Color;
            lblHeader.Text = "Quick Deposit Choices";
            lblHeader.TextAlignment = UITextAlignment.Center;
            return lblHeader;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value1, cellIdentifier);
            }

            if(indexPath.Row == quickDepositTypes.Count)
            {
                cell.TextLabel.Text = "Click here to add Quick Deposit Choice";
                cell.DetailTextLabel.Text = "";
                
            }
            else
            {
                QuickDeposit quickDeposit = quickDepositTypes[indexPath.Row];
                cell.TextLabel.Text = quickDeposit.QuickDepositType;
                cell.DetailTextLabel.Text = "$" + quickDeposit.Amount.ToString();
            }
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            if(indexPath.Row == quickDepositTypes.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
             
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    QuickDeposit deletedQuickDeposit = quickDepositTypes[indexPath.Row];
                    quickDepositTypes.Remove(deletedQuickDeposit);
                    var db = BankRepository.Connection();
                    if (db.Delete(deletedQuickDeposit) == 1)
                    {
                        tableView.ReloadData();
                    }
                    break;
            }
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return quickDepositTypes.Count + 1;
        }



        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if(indexPath.Row == quickDepositTypes.Count)
            {
                if(quickDepositTypes.Count < 5)
                {
                    callingController.NewQuickDeposit();
                }
                else if(quickDepositTypes.Count >= 5)
                {
                    callingController.MaxQuickDepositsReached();
                }
                
            }
        }
    }
}
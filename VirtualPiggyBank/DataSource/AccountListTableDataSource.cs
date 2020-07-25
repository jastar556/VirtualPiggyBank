using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using VirtualPiggyBank.Core;
using VirtualPiggyBank.Core.Repo;

namespace VirtualPiggyBank.DataSource
{
    public class AccountListTableDataSource : UITableViewSource
    {
        List<Account> Accounts = new List<Account>();

        NSString cellIdentifier = new NSString("ComponentCell");

        AccountListViewController callingController;

        public AccountListTableDataSource(AccountListViewController CallingController)
        {
            callingController = CallingController;
            Accounts = callingController.Accounts;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            


            if(cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value1, cellIdentifier);
            }

            if (indexPath.Row == Accounts.Count)
            {
                cell.TextLabel.Text = "Click here to add new piggy bank";
                cell.DetailTextLabel.Text = "";
            }
            else
            {
                Account account = Accounts[indexPath.Row];
                cell.TextLabel.Text = account.Name;
                cell.DetailTextLabel.Text = "$" + account.Balance.ToString();
            }

            return cell;
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row == Accounts.Count)
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
                    Account account = Accounts[indexPath.Row];
                    Accounts.Remove(account);
                    var db = BankRepository.Connection();
                    if (db.Delete(account) == 1)
                    {
                        tableView.ReloadData();
                    }
                    break;
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Accounts.Count + 1;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {

            if(indexPath.Row == Accounts.Count)
            {
                callingController.NewAccount();
            }
            else
            {
                callingController.AccountSelected(Accounts[indexPath.Row]);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using VirtualPiggyBank.Core;

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
                cell.TextLabel.Text = "New piggy bank";
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

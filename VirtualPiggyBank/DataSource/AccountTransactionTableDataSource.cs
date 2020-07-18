using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using VirtualPiggyBank.Core;
using VirtualPiggyBank.Core.Repo;

namespace VirtualPiggyBank.DataSource
{
    public class AccountTransactionTableDataSource : UITableViewSource
    {
        List<Transaction> Transactions = new List<Transaction>();
        AccountViewController callingController;

        NSString cellIdentifier = new NSString("ComponentCell");

        

        public AccountTransactionTableDataSource(AccountViewController CallingController)
        {
            var db = BankRepository.Connection();

            callingController = CallingController;

            string selectStatement = "SELECT * FROM Transactions WHERE Account = " + "'" + callingController.Account.Id.ToString() + "'";

            Transactions = db.CreateCommand(selectStatement).ExecuteQuery<Transaction>();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value1, cellIdentifier);
            }

            Transaction transaction = Transactions[indexPath.Row];
            cell.TextLabel.Text = transaction.Name;
            cell.DetailTextLabel.Text = "$" + transaction.TransAmount.ToString();

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Transactions.Count;
        }
    }
}

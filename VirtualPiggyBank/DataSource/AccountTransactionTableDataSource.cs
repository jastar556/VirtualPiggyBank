using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;
using VirtualPiggyBank.Core;
using VirtualPiggyBank.Core.Repo;
using System.Linq;

namespace VirtualPiggyBank.DataSource
{
    public class AccountTransactionTableDataSource : UITableViewSource
    {
        List<Transaction> Transactions = new List<Transaction>();
        

        string[] keys;
        Dictionary<string, List<Transaction>> sections = new Dictionary<string, List<Transaction>>();

        AccountViewController callingController;



        NSString cellIdentifier = new NSString("ComponentCell");

        

        public AccountTransactionTableDataSource(AccountViewController CallingController)
        {
            var db = BankRepository.Connection();

            callingController = CallingController;

            string selectStatement = "SELECT * FROM Transactions WHERE Account = " + "'" + callingController.Account.Id.ToString() + "'";

            Transactions = db.CreateCommand(selectStatement).ExecuteQuery<Transaction>();

            //foreach (Transaction transaction in Transactions)
            //{
            //    if (Dates.ContainsKey(transaction.Date.Date))
            //    {
            //        Dates[transaction.Date]++;
            //    }
            //    else
            //    {
            //        Dates.Add(transaction.Date.Date, 1);
            //    }
            //}

            foreach (Transaction transaction in Transactions)
            {
                string date = transaction.Date.ToShortDateString();
                if (sections.ContainsKey(date))
                {
                    sections[date].Add(transaction);
                }
                else
                {
                    sections.Add(date, new List<Transaction>());
                    sections[date].Add(transaction);
                }
            }

            keys = sections.Keys.ToArray();

        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            // I just swapped Height | Width in following line
            UILabel lblHeader = new UILabel(new CGRect(1, 1, tableView.SectionHeaderHeight, tableView.Frame.Width));
            lblHeader.Font = UIFont.FromName("TimesNewRomanPS-BoldMT", 20);
            lblHeader.BackgroundColor = UIColor.SystemGray2Color;
            lblHeader.Text = keys[section];
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


            Transaction transaction = sections[keys[indexPath.Section]][indexPath.Row];

            cell.TextLabel.Text = transaction.Name;
            cell.DetailTextLabel.Text = "$" + transaction.TransAmount.ToString();
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return keys.Length;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return sections[keys[section]].Count;
        }
    }
}

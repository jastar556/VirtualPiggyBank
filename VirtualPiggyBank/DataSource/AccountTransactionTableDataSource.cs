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
        

        DateTime[] keys;
        Dictionary<DateTime, List<Transaction>> sections = new Dictionary<DateTime, List<Transaction>>();

        AccountViewController callingController;



        NSString cellIdentifier = new NSString("ComponentCell");

        

        public AccountTransactionTableDataSource(AccountViewController CallingController)
        {
            var db = BankRepository.Connection();

            callingController = CallingController;

            string selectStatement = "SELECT * FROM Transactions WHERE Account = " + "'" + callingController.Account.Id.ToString() + "'";

            Transactions = db.CreateCommand(selectStatement).ExecuteQuery<Transaction>();

            CompileSections();

        }

        void CompileSections()
        {
            foreach (Transaction transaction in Transactions)
            {
                DateTime date = transaction.Date.Date;
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

            SortSections();
            
        }

        void SortSections()
        {
            sections = sections.OrderByDescending(section => section.Key).ToDictionary(obj => obj.Key, obj => obj.Value);

            keys = sections.Keys.ToArray();

            foreach (DateTime key in keys)
            {
                List<Transaction> unsortedList = sections[key];

                List<Transaction> sortedList = unsortedList.OrderByDescending(obj => obj.Date).ToList();

                sections[key] = sortedList;
            }
            
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            // I just swapped Height | Width in following line
            UILabel lblHeader = new UILabel(new CGRect(1, 1, tableView.SectionHeaderHeight, tableView.Frame.Width));
            lblHeader.Font = UIFont.FromName("TimesNewRomanPS-BoldMT", 20);
            lblHeader.BackgroundColor = UIColor.SystemGray2Color;
            lblHeader.Text = keys[section].ToShortDateString();
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

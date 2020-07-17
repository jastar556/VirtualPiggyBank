using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using VirtualPiggyBank.Core;
using VirtualPiggyBank.DataSource;

namespace VirtualPiggyBank
{
    public partial class AccountListViewController : UITableViewController
    {

        public List<Account> Accounts = new List<Account>();

        public AccountListViewController (IntPtr handle) : base (handle)
        {
           
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            populateTestAccounts();

            TableView.Source = new AccountListTableDataSource(this);
        }

        void populateTestAccounts()
        {
            Accounts.Add(new Account());
            Accounts[0].Name = "Teddy";
            Accounts[0].Balance = 50.43;
            Accounts[0].Transactions = new List<Transaction>();
            Accounts[0].Transactions.Add(new Transaction());
            Accounts[0].Transactions[0].Name = "Birthday";
            Accounts[0].Transactions[0].Date = DateTime.Today;
            Accounts[0].Transactions[0].TransAmount = -50;
        }
    }
}
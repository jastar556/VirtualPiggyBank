using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using VirtualPiggyBank.Core;
using VirtualPiggyBank.Core.Model;
using VirtualPiggyBank.Core.Repo;
using VirtualPiggyBank.Core.Service;
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

            
            NSNotificationCenter.DefaultCenter.AddObserver((NSString)"ReloadPage", reloadPage);

            var db = BankRepository.Connection();

            db.CreateTable<Account>();
            db.CreateTable<Transaction>();
            db.CreateTable<QuickDeposit>();

            Accounts = db.CreateCommand("SELECT * FROM UserAccounts").ExecuteQuery<Account>();

            TableView.Source = new AccountListTableDataSource(this);
        }

        public void AccountSelected(Account SelectedAccount)
        {

            AccountViewController accountViewController =
                this.Storyboard.InstantiateViewController("AccountViewController") as AccountViewController;

            if(accountViewController != null)
            {
                accountViewController.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;
                accountViewController.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
                accountViewController.Account = SelectedAccount;

                PresentViewController(accountViewController, true, null);
            } 
        }

        void reloadPage(NSNotification notification)
        {
            var db = BankRepository.Connection();
            Accounts = db.CreateCommand("SELECT * FROM UserAccounts").ExecuteQuery<Account>();

            TableView.Source = new AccountListTableDataSource(this);
            TableView.ReloadData();

        }
    }
}
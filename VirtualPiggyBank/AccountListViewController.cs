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

            SettingsButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                SettingsViewController settingsViewController = this.Storyboard.InstantiateViewController("SettingsViewController") as SettingsViewController;
                settingsViewController.ModalPresentationStyle = UIModalPresentationStyle.Automatic;
                settingsViewController.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;
                PresentViewController(settingsViewController, true, null);
            };

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

        public void NewAccount()
        {
            Account NewAccount = new Account();

            NewAccount.Balance = 0;
            NewAccount.Id = Guid.NewGuid();

            var NewAccountAlertController = UIAlertController.Create("New Account", "What is the name of the new piggybank?", UIAlertControllerStyle.Alert);
            NewAccountAlertController.AddTextField(field => { });
            NewAccountAlertController.AddAction(UIAlertAction.Create("Add", UIAlertActionStyle.Default, action =>
            {
                if(NewAccountAlertController.TextFields[0].Text != "")
                {
                    NewAccount.Name = NewAccountAlertController.TextFields[0].Text;
                    var db = BankRepository.Connection();
                    db.Insert(NewAccount);
                    reloadPage(null);
                }
                else
                {
                    var ErrorAlertController = UIAlertController.Create("Error", "You must enter a name in order to save a new piggybank", UIAlertControllerStyle.Alert);
                    ErrorAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                    PresentViewController(ErrorAlertController, true, null);
                }
                
            }));
            PresentViewController(NewAccountAlertController, true, null);

            
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
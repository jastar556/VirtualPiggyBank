using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using VirtualPiggyBank.Core;
using VirtualPiggyBank.Core.Model;
using VirtualPiggyBank.Core.Repo;
using VirtualPiggyBank.Core.Service;
using VirtualPiggyBank.DataSource;

namespace VirtualPiggyBank
{
    public partial class AccountViewController : UIViewController
    {
        public Account Account;
        

        public AccountViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NSNotificationCenter.DefaultCenter.AddObserver((NSString) "ReloadPage", reloadPage);

            var db = BankRepository.Connection();

            AccountNameLabel.Text = Account.Name;
            BalanceLabel.Text = "$" + Account.Balance.ToString();


            TransactionTableView.Source = new AccountTransactionTableDataSource(this);

            BackButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                DismissModalViewController(true);
            };

            DepositButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                TransactionViewController transactionViewController =
                    this.Storyboard.InstantiateViewController("TransactionViewController") as TransactionViewController;

                if(transactionViewController != null)
                {
                    transactionViewController.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;
                    transactionViewController.ModalPresentationStyle = UIModalPresentationStyle.Automatic;
                    transactionViewController.account = Account;
                    transactionViewController.TransactionType = "Deposit";

                    PresentViewController(transactionViewController, true, null);
                }
            };

            WithdrawlButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                TransactionViewController transactionViewController =
                    this.Storyboard.InstantiateViewController("TransactionViewController") as TransactionViewController;

                if (transactionViewController != null)
                {
                    transactionViewController.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;
                    transactionViewController.ModalPresentationStyle = UIModalPresentationStyle.Automatic;
                    transactionViewController.account = Account;
                    transactionViewController.TransactionType = "Withdrawal";

                    PresentViewController(transactionViewController, true, null);
                }
            };

            QuickDepositButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                List<QuickDeposit> quickDeposits = db.CreateCommand("SELECT * FROM QuickDepositTypes").ExecuteQuery<QuickDeposit>();

                UIAlertController QuickDepositAlertController = UIAlertController.Create("Quick Deposit", "Select quick deposit option", UIAlertControllerStyle.Alert);

                foreach(QuickDeposit quickDeposit in quickDeposits)
                {
                    QuickDepositAlertController.AddAction(UIAlertAction.Create(quickDeposit.QuickDepositType, UIAlertActionStyle.Default, Action => {
                        QuickDepositTransaction(quickDeposit.QuickDepositType);
                    }));
                }
                QuickDepositAlertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
                PresentViewController(QuickDepositAlertController, true, null);
            };
        }

        void QuickDepositTransaction(string QuickDepositType)
        {

        }

        void reloadPage(NSNotification notification)
        {
            var db = BankRepository.Connection();
            Account = db.Get<Account>(Account.Id);

            BalanceLabel.Text = "$" + Account.Balance.ToString();

            TransactionTableView.Source = new AccountTransactionTableDataSource(this);
            TransactionTableView.ReloadData();
        }
    }
}
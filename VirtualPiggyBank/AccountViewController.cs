using System;
using Foundation;
using UIKit;
using VirtualPiggyBank.Core;
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

            var db = BankRepository.Connection();

            AccountNameLabel.Text = Account.Name;
            BalanceLabel.Text = "$" + Account.Balance.ToString();


            TransactionTableView.Source = new AccountTransactionTableDataSource(this);

            BackButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                DismissModalViewController(true);
            };
        }
    }
}
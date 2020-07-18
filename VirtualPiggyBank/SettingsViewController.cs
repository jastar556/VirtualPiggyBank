using Foundation;
using System;
using UIKit;
using VirtualPiggyBank.DataSource;

namespace VirtualPiggyBank
{
    public partial class SettingsViewController : UIViewController
    {
        public SettingsViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            QuickDepositTableView.Source = new QuickDepositTableDataSource(this);
        }
    }
}
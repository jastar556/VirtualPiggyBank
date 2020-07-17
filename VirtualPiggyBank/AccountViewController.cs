using System;
using Foundation;
using UIKit;
using VirtualPiggyBank.Core;

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

            AccountNameLabel.Text = Account.Name;
            // Perform any additional setup after loading the view, typically from a nib.

            BackButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                DismissModalViewController(true);
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
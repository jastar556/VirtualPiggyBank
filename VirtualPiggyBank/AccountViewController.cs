using Foundation;
using System;
using UIKit;
using VirtualPiggyBank.Core;

namespace VirtualPiggyBank
{
    public partial class AccountViewController : UITabBarController
    {

        public Account Account;

        public AccountViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
        }
    }
}
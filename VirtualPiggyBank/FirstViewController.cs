using System;
using Foundation;
using UIKit;
using VirtualPiggyBank.Core;

namespace VirtualPiggyBank
{
    public partial class FirstViewController : UIViewController
    {
        Account Account;

        public FirstViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
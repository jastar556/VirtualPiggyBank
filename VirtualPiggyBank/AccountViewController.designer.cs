// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace VirtualPiggyBank
{
    [Register ("FirstViewController")]
    partial class AccountViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AccountNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DepositButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton WithdrawlButton { get; set; }

        [Action ("UIButton463_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton463_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (AccountNameLabel != null) {
                AccountNameLabel.Dispose ();
                AccountNameLabel = null;
            }

            if (DepositButton != null) {
                DepositButton.Dispose ();
                DepositButton = null;
            }

            if (WithdrawlButton != null) {
                WithdrawlButton.Dispose ();
                WithdrawlButton = null;
            }
        }
    }
}
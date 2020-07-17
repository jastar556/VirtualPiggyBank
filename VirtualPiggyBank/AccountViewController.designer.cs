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
    [Register ("AccountViewController")]
    partial class AccountViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AccountNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BackButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DepositButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView TransactionTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton WithdrawlButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AccountNameLabel != null) {
                AccountNameLabel.Dispose ();
                AccountNameLabel = null;
            }

            if (BackButton != null) {
                BackButton.Dispose ();
                BackButton = null;
            }

            if (DepositButton != null) {
                DepositButton.Dispose ();
                DepositButton = null;
            }

            if (TransactionTableView != null) {
                TransactionTableView.Dispose ();
                TransactionTableView = null;
            }

            if (WithdrawlButton != null) {
                WithdrawlButton.Dispose ();
                WithdrawlButton = null;
            }
        }
    }
}
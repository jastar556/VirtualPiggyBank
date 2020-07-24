// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace VirtualPiggyBank
{
    [Register ("TransactionViewController")]
    partial class TransactionViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AccountNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AmountLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField AmountTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SubmitButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TransactionNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TransactionNameTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TransactionTypeLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AccountNameLabel != null) {
                AccountNameLabel.Dispose ();
                AccountNameLabel = null;
            }

            if (AmountLabel != null) {
                AmountLabel.Dispose ();
                AmountLabel = null;
            }

            if (AmountTextField != null) {
                AmountTextField.Dispose ();
                AmountTextField = null;
            }

            if (SubmitButton != null) {
                SubmitButton.Dispose ();
                SubmitButton = null;
            }

            if (TransactionNameLabel != null) {
                TransactionNameLabel.Dispose ();
                TransactionNameLabel = null;
            }

            if (TransactionNameTextField != null) {
                TransactionNameTextField.Dispose ();
                TransactionNameTextField = null;
            }

            if (TransactionTypeLabel != null) {
                TransactionTypeLabel.Dispose ();
                TransactionTypeLabel = null;
            }
        }
    }
}
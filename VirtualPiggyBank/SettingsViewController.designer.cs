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
    [Register ("SettingsViewController")]
    partial class SettingsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView QuickDepositTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (QuickDepositTableView != null) {
                QuickDepositTableView.Dispose ();
                QuickDepositTableView = null;
            }
        }
    }
}
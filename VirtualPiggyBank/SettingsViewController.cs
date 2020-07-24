using Foundation;
using System;
using UIKit;
using VirtualPiggyBank.Core.Model;
using VirtualPiggyBank.Core.Repo;
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

        internal void MaxQuickDepositsReached()
        {
            var MaxQuickDepositsAlert = UIAlertController.Create("Error", "Oops! You have reached the max number of Quick Deposit choices allowed. Delete one before creating a new one.", UIAlertControllerStyle.Alert);
            MaxQuickDepositsAlert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(MaxQuickDepositsAlert, true, null);
            QuickDepositTableView.ReloadData();
        }

        internal void NewQuickDeposit()
        {
            QuickDeposit newQuickDeposit = new QuickDeposit();

            var QuickDepositNameAlert = UIAlertController.Create("Quick Deposit Name", "What is the name of the new Quick Deposit type?", UIAlertControllerStyle.Alert);
            QuickDepositNameAlert.AddTextField(field => { });
            QuickDepositNameAlert.TextFields[0].AutocapitalizationType = UITextAutocapitalizationType.Sentences;
            QuickDepositNameAlert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, action =>
            {
                if(QuickDepositNameAlert.TextFields[0].Text != "")
                {
                    newQuickDeposit.QuickDepositType = QuickDepositNameAlert.TextFields[0].Text;

                    var QuickDepositAmountAlert = UIAlertController.Create("Quick Deposit Amount", "What is the amount paid for " + newQuickDeposit.QuickDepositType + "?", UIAlertControllerStyle.Alert);
                    QuickDepositAmountAlert.AddTextField(field => { });
                    QuickDepositAmountAlert.TextFields[0].KeyboardType = UIKeyboardType.DecimalPad;
                    QuickDepositAmountAlert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, Action =>
                    {
                        if (QuickDepositAmountAlert.TextFields[0].Text != "")
                        {
                            newQuickDeposit.Amount = Double.Parse(QuickDepositAmountAlert.TextFields[0].Text);
                            var db = BankRepository.Connection();
                            db.Insert(newQuickDeposit);
                            QuickDepositTableView.Source = new QuickDepositTableDataSource(this);
                            QuickDepositTableView.ReloadData();
                        }
                        else
                        {
                            var ErrorAlertController = UIAlertController.Create("Error", "Amount can not be blank", UIAlertControllerStyle.Alert);
                            ErrorAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                            PresentViewController(ErrorAlertController, true, null);
                        }
                    }));
                    QuickDepositAmountAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
                    PresentViewController(QuickDepositAmountAlert, true, null);
                }
                else
                {
                    var ErrorAlertController = UIAlertController.Create("Error", "Name can not be blank", UIAlertControllerStyle.Alert);
                    ErrorAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                    PresentViewController(ErrorAlertController, true, null);
                }
            }));
            QuickDepositNameAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

            PresentViewController(QuickDepositNameAlert, true, null);

            QuickDepositTableView.ReloadData();

        }
    }
}
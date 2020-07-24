using Foundation;
using System;
using UIKit;
using VirtualPiggyBank.Core;
using VirtualPiggyBank.Core.Repo;
using VirtualPiggyBank.Core.Service;

namespace VirtualPiggyBank
{
    public partial class TransactionViewController : UIViewController
    {
        public string TransactionType { get; set; }

        public Account account { get; set; }

        public TransactionViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            var db = BankRepository.Connection();

            base.ViewDidLoad();

            AccountNameLabel.Text = account.Name;

            TransactionTypeLabel.Text = TransactionType;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            View.AddGestureRecognizer(g);

            AmountTextField.KeyboardType = UIKeyboardType.NumberPad;
            TransactionNameTextField.ShouldReturn = (textField) =>
            {
                TransactionNameTextField.EndEditing(true);
                return true;
            };
            
            SubmitButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                if(TransactionNameTextField.Text != "" && AmountTextField.Text != "")
                {
                    Transaction newTransaction = new Transaction();
                    newTransaction.TransactionID = Guid.NewGuid();
                    newTransaction.Account = account.Id;
                    newTransaction.Date = DateTime.Today;
                    newTransaction.Name = TransactionNameTextField.Text;
                    newTransaction.TransAmount = double.Parse(AmountTextField.Text);
                    newTransaction.Note = NoteTextfield.Text;

                    string confirmationString;

                    if (TransactionType == "Deposit")
                    {
                        confirmationString = "Confirm you are depositing $" + newTransaction.TransAmount + " into account: " + account.Name + ".";
                    }
                    else
                    {
                        confirmationString = "Confirm you are withdrawing $" + newTransaction.TransAmount + " from account: " + account.Name + ".";
                    }

                    var ConfirmationAlertController = UIAlertController.Create("Confirmation", confirmationString, UIAlertControllerStyle.Alert);
                    ConfirmationAlertController.AddAction(UIAlertAction.Create("Confirm", UIAlertActionStyle.Default, Action =>
                    {
                        if(TransactionType == "Withdrawal")
                        {
                            newTransaction.TransAmount = newTransaction.TransAmount - (newTransaction.TransAmount * 2);
                        }
                        var completed = db.Insert(newTransaction);
                        
                        if (completed > 0)
                        {
                            //updated balance in account table
                            account.Balance = AccountCalculations.CalculateBalance(account);
                            db.InsertOrReplace(account);
                            NSNotificationCenter.DefaultCenter.PostNotificationName("ReloadPage", null);
                            //confirmation dialog
                            var CompletionAlertController = UIAlertController.Create("Transaction Complete", "The transaction has been completed", UIAlertControllerStyle.Alert);
                            CompletionAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, action => { DismissModalViewController(true); }));
                            PresentViewController(CompletionAlertController, true, null);
                            
                        }
                        
                    }));
                    ConfirmationAlertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
                    PresentViewController(ConfirmationAlertController, true, null);
                }
            };

        }
    }
}
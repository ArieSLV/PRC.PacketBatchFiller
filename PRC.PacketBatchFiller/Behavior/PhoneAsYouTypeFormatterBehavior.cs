using System.Windows.Input;
using System.Windows.Interactivity;
using Catel.IoC;
using Catel.MVVM;
using PhoneNumbers;
using Xceed.Wpf.Toolkit;


namespace PRC.PacketBatchFiller.Behavior
{
    public class PhoneAsYouTypeFormatterBehavior : Behavior<WatermarkTextBox>
    {
        private static readonly PhoneNumberUtil PhoneUtil = PhoneNumberUtil.GetInstance();
        private static readonly AsYouTypeFormatter AsYouTypeFormatter = PhoneUtil.GetAsYouTypeFormatter("RU");

        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
        }

        public void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                case Key.Tab:
                    var request = new TraversalRequest(FocusNavigationDirection.Next) { Wrapped = true };
                    AssociatedObject.MoveFocus(request);
                    break;
                default:
                    var phoneNumberPrototype = new PhoneNumber();
                    
                    var phoneNumberText = "";
                    AsYouTypeFormatter.Clear();
                    foreach (var character in AssociatedObject.Text)
                    {
                        AsYouTypeFormatter.InputDigit(character);
                    }

                    switch (e.Key)
                    {
                        case Key.D0:
                        case Key.D1:
                        case Key.D2:
                        case Key.D3:
                        case Key.D4:
                        case Key.D5:
                        case Key.D6:
                        case Key.D7:
                        case Key.D8:
                        case Key.D9:

                            phoneNumberText = AsYouTypeFormatter.InputDigit(e.Key.ToString()[1]);
                            break;

                        case Key.NumPad0:
                        case Key.NumPad1:
                        case Key.NumPad2:
                        case Key.NumPad3:
                        case Key.NumPad4:
                        case Key.NumPad5:
                        case Key.NumPad6:
                        case Key.NumPad7:
                        case Key.NumPad8:
                        case Key.NumPad9:

                            phoneNumberText = AsYouTypeFormatter.InputDigit(e.Key.ToString()[6]);
                            break;

                        case Key.Add:
                        case Key.OemPlus:
                            phoneNumberText = AsYouTypeFormatter.InputDigit('+');
                            break;
                    }
                    

                    try { phoneNumberPrototype = PhoneUtil.Parse(phoneNumberText, "RU"); }
                    catch (NumberParseException exc)
                    {
                        if (exc.ErrorType != ErrorType.NOT_A_NUMBER && exc.ErrorType != ErrorType.TOO_SHORT_NSN)
                            MessageBox.Show(exc.ToString());
                    }

                    if (PhoneUtil.IsPossibleNumberWithReason(phoneNumberPrototype) != PhoneNumberUtil.ValidationResult.TOO_LONG)
                    {
                        var dependencyResolver = IoCConfiguration.DefaultDependencyResolver;
                        var commandManager = dependencyResolver.Resolve<ICommandManager>();

                        var addCharToPhoneCommand = commandManager.GetCommand("AddCharToPhoneCommand");
                        addCharToPhoneCommand.Execute(e.Key);
                        AssociatedObject.CaretIndex = AssociatedObject.Text.Length;
                    }
                    break;
            }
            e.Handled = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
        }
    }
}
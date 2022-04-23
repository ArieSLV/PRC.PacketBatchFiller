using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Behavior
{
    class SetFocusOnHiddenMaskedTextBox : TriggerAction<TextBlock>
    {
        public static readonly DependencyProperty MaskedTextBoxNameProperty = DependencyProperty.Register(
            "MaskedTextBoxName", typeof (MaskedTextBox), typeof (SetFocusOnHiddenMaskedTextBox), new PropertyMetadata(default(MaskedTextBox)));

        public MaskedTextBox MaskedTextBoxName
        {
            get { return (MaskedTextBox) GetValue(MaskedTextBoxNameProperty); }
            set { SetValue(MaskedTextBoxNameProperty, value); }
        }


        protected override void Invoke(object parameter)
        {
            AssociatedObject.Foreground = SystemColors.ControlTextBrush;
            if (!AssociatedObject.Text.Contains(":")) AssociatedObject.Text += ":";

            MaskedTextBoxName.Visibility = Visibility.Visible;
            MaskedTextBoxName.Focus();
        }
    }
}
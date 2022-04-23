using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Behavior
{
    class HideAndMakeDefaultViewOfTextBlock : TriggerAction<MaskedTextBox>
    {
        public static readonly DependencyProperty TextBlockProperty = DependencyProperty.Register(
            "TextBlock", typeof (TextBlock), typeof (HideAndMakeDefaultViewOfTextBlock),
            new PropertyMetadata(default(TextBlock)));

        public TextBlock TextBlock
        {
            get { return (TextBlock) GetValue(TextBlockProperty); }
            set { SetValue(TextBlockProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (string.IsNullOrEmpty(AssociatedObject.GetRawText()))
            {
                AssociatedObject.Visibility = Visibility.Collapsed;

                if (TextBlock.Text.Substring(TextBlock.Text.Length - 1, 1) == ":") TextBlock.Text = TextBlock.Text.Substring(0, TextBlock.Text.Length - 1);

                TextBlock.Foreground = Brushes.Red;
            }
        }
    }
}
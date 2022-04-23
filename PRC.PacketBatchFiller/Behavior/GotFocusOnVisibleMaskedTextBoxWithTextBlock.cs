using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Behavior
{
    class GotFocusOnVisibleMaskedTextBoxWithTextBlock : Behavior<UIElement>
    {
        public static readonly DependencyProperty MaskedTextBoxProperty = DependencyProperty.Register(
            "MaskedTextBox", typeof (MaskedTextBox), typeof (GotFocusOnVisibleMaskedTextBoxWithTextBlock), new PropertyMetadata(default(MaskedTextBox)));

        public MaskedTextBox MaskedTextBox
        {
            get { return (MaskedTextBox) GetValue(MaskedTextBoxProperty); }
            set { SetValue(MaskedTextBoxProperty, value); }
        }

        public static readonly DependencyProperty TextBlockProperty = DependencyProperty.Register(
            "TextBlock", typeof (TextBlock), typeof (GotFocusOnVisibleMaskedTextBoxWithTextBlock), new PropertyMetadata(default(TextBlock)));

        public TextBlock TextBlock
        {
            get { return (TextBlock) GetValue(TextBlockProperty); }
            set { SetValue(TextBlockProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
        }

        private void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key !=Key.Tab) return;

            TextBlock.Foreground = SystemColors.ControlTextBrush;
            
            if (!TextBlock.Text.Contains(":")) TextBlock.Text += ":";

            MaskedTextBox.Visibility = Visibility.Visible;
            MaskedTextBox.Focus();

            e.Handled = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
        }


        

        
        
    }
}
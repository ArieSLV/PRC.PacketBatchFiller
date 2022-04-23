using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PRC.PacketBatchFiller.Behavior
{
    class ListBoxItemTextToTextBox : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += AssociatedObjectOnPreviewKeyDown;
        }

        private void AssociatedObjectOnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if ((!Keyboard.IsKeyDown(Key.Enter) && !Keyboard.IsKeyDown(Key.Space)) || AssociatedObject.SelectedItem == null) return;

            TextBox.Text = AssociatedObject.SelectedItem + " ";
            TextBox.Focus();
            TextBox.CaretIndex = TextBox.Text.Length;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= AssociatedObjectOnPreviewKeyDown;
        }

        public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
            "TextBox", typeof (TextBox), typeof (ListBoxItemTextToTextBox), new PropertyMetadata(default(TextBox)));

        public TextBox TextBox
        {
            get { return (TextBox) GetValue(TextBoxProperty); }
            set { SetValue(TextBoxProperty, value); }
        }
    }
}

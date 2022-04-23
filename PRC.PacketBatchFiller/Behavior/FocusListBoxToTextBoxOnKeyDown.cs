using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PRC.PacketBatchFiller.Behavior
{
    class FocusListBoxToTextBoxOnKeyDown : Behavior<ListBox>
    {
        public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
            "TextBox", typeof (TextBox), typeof (FocusListBoxToTextBoxOnKeyDown), new PropertyMetadata(default(TextBox)));

        public TextBox TextBox
        {
            get { return (TextBox) GetValue(TextBoxProperty); }
            set { SetValue(TextBoxProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += AssociatedObjectOnPreviewKeyDown;
        }

        private void AssociatedObjectOnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (!Keyboard.IsKeyDown(Key.Up) || AssociatedObject.Items.IndexOf(AssociatedObject.SelectedItem) != 0) return;

            TextBox.Focus();
            TextBox.CaretIndex = TextBox.Text.Length;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= AssociatedObjectOnPreviewKeyDown;
        }
    }
}
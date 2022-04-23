using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using Catel.Data;

namespace PRC.PacketBatchFiller.Behavior
{
    internal class FocusTextBoxToListBoxOnKeyDown : TriggerAction<UIElement>
    {

        public static readonly DependencyProperty KeyProperty = 
            DependencyProperty.Register("Key", typeof (Key), typeof (FocusTextBoxToListBoxOnKeyDown));

        public Key Key
        {
            get { return (Key) GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty = 
            DependencyProperty.Register("Target", typeof (UIElement), typeof (FocusTextBoxToListBoxOnKeyDown));

        public UIElement Target
        {
            get { return (UIElement) GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (!Keyboard.IsKeyDown(Key)) return;

            var listBox = Target as ListBox;
            if (listBox?.Items.Count == 1) return;

            Target.Focus();
        }
    }
}
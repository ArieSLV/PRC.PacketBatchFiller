using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Behavior
{
    internal class FocusToNextControlOnMaskIsFull : Behavior<MaskedTextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (AssociatedObject?.Value == null || AssociatedObject.SelectedText != string.Empty || AssociatedObject.Value as DateTime? == default(DateTime)) return;

            var request = new TraversalRequest(FocusNavigationDirection.Next) { Wrapped = true };
            AssociatedObject.MoveFocus(request);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }
    }
}
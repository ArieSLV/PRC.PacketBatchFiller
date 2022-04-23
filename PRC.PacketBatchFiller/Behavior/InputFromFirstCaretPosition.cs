using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Behavior
{
    public class InputFromFirstCaretPosition : Behavior<MaskedTextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += AssociatedObjectOnPreviewKeyDown;
        }

        private void AssociatedObjectOnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (string.IsNullOrEmpty(AssociatedObject.GetRawText()) && AssociatedObject.CaretIndex != 0)
            {
                AssociatedObject.CaretIndex = 0;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= AssociatedObjectOnPreviewKeyDown;
        }
    }
}
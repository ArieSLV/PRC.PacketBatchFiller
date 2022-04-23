using System;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PRC.PacketBatchFiller.Behavior
{
    public class TabOnEnterOrTabBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
        }

        private void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            var request = new TraversalRequest(FocusNavigationDirection.Next) {Wrapped = true};
            AssociatedObject.MoveFocus(request);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
        }
    }
}
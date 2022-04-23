using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace PRC.PacketBatchFiller.Behavior
{
    internal class MoveFocusToConcreteTextBoxAfterSuggestComplete : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.IsVisibleChanged += AssociatedObjectOnIsVisibleChanged;
        }

        private void AssociatedObjectOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (AssociatedObject.IsVisible)
            {
                AssociatedObject.Focus();
            }
            else {
                var textBoxList = FindChildFrameworkElementsOfType(AssociatedObject.GetRootControl(), new List<Control>());
                var currentTabIndex = AssociatedObject.GetTabIndexOfRootUsercontrol();

                var targetTabIndex = (from textBox in textBoxList where textBox.TabIndex > currentTabIndex select textBox.TabIndex).Concat(new[] { int.MaxValue }).Min();


                foreach (var textBox in textBoxList.Where(textBox => textBox.TabIndex == targetTabIndex))
                {
                    textBox.Focus();
                    break;
                }
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.IsVisibleChanged -= AssociatedObjectOnIsVisibleChanged;
        }

        private static ICollection<T> FindChildFrameworkElementsOfType<T>(DependencyObject parent, ICollection<T> list) where T : FrameworkElement
        {

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var item = child as T;

                if (item != null)
                {
                    list.Add(item);
                }
                FindChildFrameworkElementsOfType(child, list);
            }

            return list;
        }
        
    }
}
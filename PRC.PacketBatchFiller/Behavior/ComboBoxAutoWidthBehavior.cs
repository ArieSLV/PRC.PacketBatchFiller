using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace PRC.PacketBatchFiller.Behavior
{
    public class ComboBoxAutoWidthBehavior : Behavior<ComboBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.LayoutUpdated += AssociatedObjectOnLayoutUpdated;
        }

        public void AssociatedObjectOnLayoutUpdated(object sender, EventArgs e)
        {
            const double comboBoxWidth = 17;
            double width = 0;
            if (AssociatedObject.IsDropDownOpen && AssociatedObject.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                

                foreach (var comboBoxItem in from object item in AssociatedObject.Items select AssociatedObject.ItemContainerGenerator.ContainerFromItem(item) as ComboBoxItem)
                {
                    comboBoxItem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    if (comboBoxItem.DesiredSize.Width > width)
                    {
                        width = comboBoxItem.DesiredSize.Width;
                    }
                }
                AssociatedObject.Width = comboBoxWidth + width;

            }
            else if (AssociatedObject.SelectedItem != null && AssociatedObject.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                var comboBoxItem = AssociatedObject.ItemContainerGenerator.ContainerFromItem(AssociatedObject.SelectedItem) as ComboBoxItem;
                comboBoxItem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                if (comboBoxItem.DesiredSize.Width > width)
                {
                    width = comboBoxItem.DesiredSize.Width;
                }
                AssociatedObject.Width = comboBoxWidth + width;
            }

            
        }


        protected override void OnDetaching()
        {
            AssociatedObject.LayoutUpdated -= AssociatedObjectOnLayoutUpdated;
        }
    }


}
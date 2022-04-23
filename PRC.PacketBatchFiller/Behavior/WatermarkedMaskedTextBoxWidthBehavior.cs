using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Behavior
{
    public class WatermarkedMaskedTextBoxWidthBehavior : Behavior<MaskedTextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (AssociatedObject.GetRawText() == string.Empty)
            {
                //Setting adornedElement to adorner size;
                //Некий костыль из-за странного поведения DataBinding у контрола с датой
                var dc = AssociatedObject.DataContext;

                if (dc != null)
                {
                    var text = TypeDescriptor.GetProperties(dc)["LabelText"].GetValue(dc) ?? "";

                    var textBlock = new TextBlock { Text = text.ToString(), TextWrapping = TextWrapping.Wrap };
                    textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    textBlock.Arrange(new Rect(textBlock.DesiredSize));

                    AssociatedObject.Width = textBlock.ActualWidth + 4;
                }
            }
            else
            {
                AssociatedObject.Width = double.NaN;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }
    }
}
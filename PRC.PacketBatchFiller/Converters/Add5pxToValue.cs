using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Converters
{
    public class Add5PxToValue : ConvertorBase<Add5PxToValue>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = (string) value;
            var textBlock = new TextBlock {Text = incomingValue, TextWrapping = TextWrapping.Wrap};
            textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            textBlock.Arrange(new Rect(textBlock.DesiredSize));
            
            return textBlock.ActualWidth + 4;
        }
    }
}
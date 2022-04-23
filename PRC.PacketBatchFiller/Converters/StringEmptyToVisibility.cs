using System;
using System.Globalization;
using System.Windows;

namespace PRC.PacketBatchFiller.Converters
{
    public class StringEmptyToVisibility : ConvertorBase<StringEmptyToVisibility>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = value as string;

            if (string.IsNullOrWhiteSpace(incomingValue) || incomingValue == string.Empty) return Visibility.Collapsed;

            return Visibility.Visible;
        }
    }
}
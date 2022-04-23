using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace PRC.PacketBatchFiller.Converters
{
    internal class TextBlockValueToLabelVisibility : ConvertorBase<TextBlockValueToLabelVisibility>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                return (DateTime) value == default(DateTime) ? Visibility.Collapsed : Visibility.Visible;
            }

            if (value is string)
            {
                return (string) value == string.Empty ? Visibility.Collapsed : Visibility.Visible;
            }

            return value == null ? Visibility.Collapsed : Visibility.Visible;
            
        }
    }
}
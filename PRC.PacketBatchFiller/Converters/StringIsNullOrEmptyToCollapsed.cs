using System;
using System.Globalization;
using System.Windows;

namespace PRC.PacketBatchFiller.Converters
{
    class StringIsNullOrEmptyToCollapsed : ConvertorBase<StringIsNullOrEmptyToCollapsed>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty((string) value) ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
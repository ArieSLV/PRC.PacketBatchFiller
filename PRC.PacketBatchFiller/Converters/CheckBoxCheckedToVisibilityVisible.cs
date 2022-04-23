using System;
using System.Globalization;
using System.Windows;

namespace PRC.PacketBatchFiller.Converters
{
    class CheckBoxCheckedToVisibilityVisible : ConvertorBase<CheckBoxCheckedToVisibilityVisible>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = (bool)value;

            return incomingValue ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
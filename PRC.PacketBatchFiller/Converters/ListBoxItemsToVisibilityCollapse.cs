using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace PRC.PacketBatchFiller.Converters
{
    class ListBoxItemsToVisibilityCollapse : ConvertorBase<ListBoxItemsToVisibilityCollapse>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = (int) value;

            return incomingValue > 1 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

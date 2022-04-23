using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using PRC.PacketBatchFiller.Models.PersonsEntity;

namespace PRC.PacketBatchFiller.Converters
{
    class ListBoxItemsToVisibilityVisible : ConvertorBase<ListBoxItemsToVisibilityVisible>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = (int) value;
            

            if (incomingValue < 5) return Visibility.Collapsed;
            else return Visibility.Visible;
        }
    }
}
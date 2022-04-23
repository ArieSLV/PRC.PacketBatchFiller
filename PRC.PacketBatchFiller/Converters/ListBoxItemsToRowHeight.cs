using System;
using System.Collections.Generic;
using System.Globalization;
using Catel.MVVM.Converters;
using PRC.PacketBatchFiller.DataAccess.Models;

namespace PRC.PacketBatchFiller.Converters
{
    public class ListBoxItemsToRowHeight : ConvertorBase<ListBoxItemsToRowHeight>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = (int)value;
            return incomingValue > 1 ? "Auto" : "0";
        }
    }
}

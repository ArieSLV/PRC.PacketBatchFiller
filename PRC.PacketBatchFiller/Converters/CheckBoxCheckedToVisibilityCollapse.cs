using System;

using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace PRC.PacketBatchFiller.Converters
{
    class CheckBoxCheckedToVisibilityCollapse : ConvertorBase<CheckBoxCheckedToVisibilityCollapse>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = (bool) value;

            return incomingValue ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
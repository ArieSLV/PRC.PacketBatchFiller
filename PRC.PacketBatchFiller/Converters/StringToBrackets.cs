using System;
using System.Globalization;

namespace PRC.PacketBatchFiller.Converters
{
    public class StringToBrackets : ConvertorBase<StringToBrackets>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = (string) value;

            return string.IsNullOrEmpty(incomingValue) ? string.Empty : $" ({incomingValue})";
        }
    }
}
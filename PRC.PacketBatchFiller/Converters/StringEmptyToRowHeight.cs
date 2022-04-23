using System;
using System.Globalization;

namespace PRC.PacketBatchFiller.Converters
{
    public class StringEmptyToRowHeight : ConvertorBase<StringEmptyToRowHeight>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = value as string;

            if (string.IsNullOrWhiteSpace(incomingValue) || incomingValue == string.Empty) return "0";
            
            return "Auto";
        }
    }
}
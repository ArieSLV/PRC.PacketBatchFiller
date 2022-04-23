using System;
using System.Globalization;

namespace PRC.PacketBatchFiller.Converters
{
    public class AddColonToTheEndOfString : ConvertorBase<AddColonToTheEndOfString>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{(string) value}:";
        }
    }
}
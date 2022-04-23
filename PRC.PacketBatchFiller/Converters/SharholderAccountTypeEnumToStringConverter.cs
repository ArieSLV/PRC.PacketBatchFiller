using System;
using System.Globalization;
using PRC.PacketBatchFiller.Extensions;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses;

namespace PRC.PacketBatchFiller.Converters
{
    internal class SharholderAccountTypeEnumToStringConverter : ConvertorBase<SharholderAccountTypeEnumToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ShareholderAccountType)) return string.Empty;
            
            var incomingValue = (ShareholderAccountType) value;
            
            return StringEnum.GetStringValue(incomingValue);
        }
    }
}
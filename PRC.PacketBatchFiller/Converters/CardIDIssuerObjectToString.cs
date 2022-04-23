using System;
using System.Globalization;
using System.Windows.Media;
using PRC.PacketBatchFiller.Models.PersonsEntity;

namespace PRC.PacketBatchFiller.Converters
{
    public class CardIDIssuerObjectToString : ConvertorBase<CardIDIssuerObjectToString>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = value as CardIDIssuer;

            if (incomingValue == null) return "[орган выдачи паспорта не выбран]";
            return $"{incomingValue.Name}, код подразделения: {incomingValue.Code}";
        }
    }
}
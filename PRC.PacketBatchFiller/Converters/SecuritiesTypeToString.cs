using System;
using System.Globalization;
using PRC.PacketBatchFiller.Extensions;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;

namespace PRC.PacketBatchFiller.Converters
{
    class SecuritiesTypeToString : ConvertorBase<SecuritiesTypeToString>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var securitiesTypes = (SecuritiesTypes) value;

            return StringEnum.GetStringValue(securitiesTypes);
        }
    }
}
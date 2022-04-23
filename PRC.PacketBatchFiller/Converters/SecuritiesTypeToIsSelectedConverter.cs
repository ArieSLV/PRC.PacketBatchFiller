using System;
using System.Globalization;
using System.Windows.Data;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.LegalEntityEntity;

namespace PRC.PacketBatchFiller.Converters
{
    [ValueConversion(typeof(SecuritiesTypes), typeof(bool), ParameterType = typeof(SecuritiesTypes))]
    public class SecuritiesTypeToIsSelectedConverter : ConvertorBase<SecuritiesTypeToIsSelectedConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SecuritiesTypes)) return false;

            var securitiesTypeRepresented = SecuritiesTypes.Unknown;

            if (parameter is SecuritiesTypes) securitiesTypeRepresented = (SecuritiesTypes) parameter;
            else if (parameter is string)     securitiesTypeRepresented = (SecuritiesTypes) Enum.Parse(typeof(SecuritiesTypes), (string) parameter);

            var securitiesType = (SecuritiesTypes) value;

            return securitiesType == securitiesTypeRepresented;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var securitiesTypeRepresented = SecuritiesTypes.Unknown;

            if (parameter is SecuritiesTypes) securitiesTypeRepresented = (SecuritiesTypes) parameter;
            else if (parameter is string) securitiesTypeRepresented = (SecuritiesTypes) Enum.Parse(typeof(SecuritiesTypes), (string) parameter);

            var isChecked = false;
            if (value is bool) isChecked = (bool) value;
            else if (value is bool?) isChecked = ((bool?) value).HasValue ? ((bool?) value).Value : false;

            return isChecked ? securitiesTypeRepresented : Binding.DoNothing;
        }
    }
}
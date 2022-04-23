using System;
using System.Globalization;
using System.Windows.Data;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses;

namespace PRC.PacketBatchFiller.Converters
{


    [ValueConversion(typeof(ShareholderAccountType), typeof(bool), ParameterType = typeof(ShareholderAccountType))]
    public class ShareholderAccountTypeToIsSelectedConverter : ConvertorBase<ShareholderAccountTypeToIsSelectedConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ShareholderAccountType)) return false;

            var shareholderAccountTypeRepresented = ShareholderAccountType.Owner;

            if      (parameter is ShareholderAccountType) shareholderAccountTypeRepresented = (ShareholderAccountType) parameter;
            else if (parameter is string)                 shareholderAccountTypeRepresented = (ShareholderAccountType) Enum.Parse(typeof(ShareholderAccountType), (string) parameter);

            var shareholderAccountType = (ShareholderAccountType) value;
            return shareholderAccountType == shareholderAccountTypeRepresented;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var shareholderAccountTypeRepresented = ShareholderAccountType.Owner;
            if      (parameter is ShareholderAccountType) shareholderAccountTypeRepresented = (ShareholderAccountType) parameter;
            else if (parameter is string)                 shareholderAccountTypeRepresented = (ShareholderAccountType)  Enum.Parse(typeof(ShareholderAccountType), (string) parameter );

            var isChecked = false;
            if      (value is bool)  isChecked = (bool) value;
            else if (value is bool?) isChecked = ((bool?) value).HasValue ? ((bool?) value).Value : false;

            return isChecked ? shareholderAccountTypeRepresented : Binding.DoNothing;
        }
    }
}
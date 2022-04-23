using System;
using System.Globalization;
using System.Windows.Data;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.Converters
{
    [ValueConversion(typeof(DividentsPaymentWays), typeof(bool), ParameterType = typeof(DividentsPaymentWays))]
    public class DividentsPaymentWayToIsSelectedConverter : ConvertorBase<DividentsPaymentWayToIsSelectedConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DividentsPaymentWays)) return false;

            var dividentsPaymentWayRepresented = DividentsPaymentWays.Unknow;

            if (parameter is DividentsPaymentWays) dividentsPaymentWayRepresented = (DividentsPaymentWays) parameter;
            else if (parameter is string)
                dividentsPaymentWayRepresented =
                    (DividentsPaymentWays) Enum.Parse(typeof (DividentsPaymentWays), (string) parameter);

            var dividentsPaymentWay = (DividentsPaymentWays) value;

            return (dividentsPaymentWay == dividentsPaymentWayRepresented);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dividentsPaymentWayRepresented = DividentsPaymentWays.Unknow;

            if (parameter is DividentsPaymentWays)
                dividentsPaymentWayRepresented = (DividentsPaymentWays) parameter;
            else if (parameter is string)
                dividentsPaymentWayRepresented =
                    (DividentsPaymentWays) Enum.Parse(typeof (DividentsPaymentWays), (string) parameter);

            var isChecked = false;
            if (value is bool) isChecked = (bool) value;
            else if (value is bool?) isChecked = ((bool?) value).HasValue ? ((bool?) value).Value : false;

            return (isChecked) ? dividentsPaymentWayRepresented : Binding.DoNothing;
        }
    }
}
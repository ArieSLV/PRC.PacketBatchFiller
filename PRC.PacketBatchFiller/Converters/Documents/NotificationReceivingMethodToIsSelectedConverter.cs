using System;
using System.Globalization;
using System.Windows.Data;
using PRC.PacketBatchFiller.Models.BaseClasses;
using PRC.PacketBatchFiller.Models.Documents;

namespace PRC.PacketBatchFiller.Converters.Documents
{
    [ValueConversion(typeof(NotificationReceivingMethod), typeof(bool), ParameterType = typeof(NotificationReceivingMethod))]
    public class NotificationReceivingMethodToIsSelectedConverter : ConvertorBase<NotificationReceivingMethodToIsSelectedConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is NotificationReceivingMethod)) return false;

            var methodRepresented = (NotificationReceivingMethod) parameter;
            var method = (NotificationReceivingMethod) value;

            return methodRepresented == method;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var methodRepresented = (NotificationReceivingMethod)parameter;
            var isChecked = false;
            if (value is bool) isChecked = (bool)value;
            else if (value is bool?) isChecked = ((bool?)value).HasValue ? ((bool?)value).Value : false;

            return isChecked ? methodRepresented : Binding.DoNothing;
        }

    }
}
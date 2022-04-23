using System;
using System.Globalization;
using System.Windows.Data;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.Converters
{
    [ValueConversion(typeof(ContactType), typeof(bool), ParameterType = typeof(ContactType))]
    public class ContactTypeToIsSelectedConverter : ConvertorBase<ContactTypeToIsSelectedConverter>
    {
        
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ContactType)) return false;

            var phoneNumberTypeRepresented = ContactType.Work;

            if      (parameter is ContactType)   phoneNumberTypeRepresented = (ContactType) parameter;
            else if (parameter is string)            phoneNumberTypeRepresented = (ContactType) Enum.Parse(typeof (ContactType), (string) parameter);

            var phoneNumberType = (ContactType) value;

            return phoneNumberType == phoneNumberTypeRepresented;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var phoneNumberTypeRepresented = ContactType.Work;

            if      (parameter is ContactType)  phoneNumberTypeRepresented = (ContactType) parameter;
            else if (parameter is string)           phoneNumberTypeRepresented = (ContactType) Enum.Parse(typeof (ContactType), (string) parameter);

            var isChecked = false;
            if      (value is bool)  isChecked = (bool) value;
            else if (value is bool?) isChecked = ((bool?) value).HasValue ? ((bool?) value).Value : false;
            return isChecked ? phoneNumberTypeRepresented : Binding.DoNothing;
        }
    }
}
using System;
using System.Globalization;
using System.Windows;
using Catel;
using PRC.PacketBatchFiller.Models.PersonsEntity;

namespace PRC.PacketBatchFiller.Converters
{
    class NullOrDefaultToVisibilityCollapse : ConvertorBase<NullOrDefaultToVisibilityCollapse>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            const string defaultPlaceOfBirth = "[место рождения не выбрано]";
            const string defaultCardIDIssuer = "[орган выдачи паспорта не выбран]";

            var incomingPlaceOfBirth = value as PlaceOfBirth;
            var incomingCardIDIssuer = value as CardIDIssuer;
            

            if (incomingPlaceOfBirth != null && incomingPlaceOfBirth.Value == defaultPlaceOfBirth)
            {
                return Visibility.Collapsed;
            }
            if (incomingCardIDIssuer != null && incomingCardIDIssuer.Name == defaultCardIDIssuer)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }
    }
}
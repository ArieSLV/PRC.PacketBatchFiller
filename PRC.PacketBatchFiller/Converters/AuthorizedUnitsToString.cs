using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using PRC.PacketBatchFiller.Models;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.Converters
{
    public class AuthorizedUnitsToString : ConvertorBase<AuthorizedUnitsToString>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingCollection = (ObservableCollection<Unit>) value;

            var stringBuilder = new StringBuilder();

            foreach (var unit in incomingCollection)
            {
                stringBuilder.Append(unit.FullName);
                if (incomingCollection.IndexOf(unit) != incomingCollection.Count - 1) stringBuilder.Append(", ");
            }

            return stringBuilder.ToString();

        }
    }
}
using System;
using System.Globalization;
using PRC.PacketBatchFiller.Models.BaseClasses.UnitsEntity;

namespace PRC.PacketBatchFiller.Converters
{
    class ContactTypeToImage : ConvertorBase<ContactTypeToImage>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var phoneType = (ContactType) value;

            switch (phoneType)
            {
                case ContactType.Unknown:
                    return new Uri("/PRC.PacketBatchFiller;component/Resources/question_mark_100px.png", UriKind.Relative);
                case ContactType.CellPhone:
                    return new Uri("/PRC.PacketBatchFiller;component/Resources/cell_phone_100px.png", UriKind.Relative);
                case ContactType.Personal:
                    return new Uri("/PRC.PacketBatchFiller;component/Resources/home_100px.png", UriKind.Relative);
                case ContactType.Work:
                    return new Uri("/PRC.PacketBatchFiller;component/Resources/factory_100px.png", UriKind.Relative);
                case ContactType.Fax:
                    return new Uri("/PRC.PacketBatchFiller;component/Resources/fax_100px.png", UriKind.Relative);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
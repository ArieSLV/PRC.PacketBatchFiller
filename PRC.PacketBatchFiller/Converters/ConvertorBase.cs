using System;
using System.Globalization;
using System.Windows.Markup;
using Catel.MVVM.Converters;

namespace PRC.PacketBatchFiller.Converters
{
    public abstract class ConvertorBase<T> : MarkupExtension, IValueConverter where T: class, new()
    {
        private static T _converter;

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new T());
        }

        
    }
}

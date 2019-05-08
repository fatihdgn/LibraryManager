using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Fthdgn.LibraryManager.UI.Converters
{
    public class ValueConverter<TFrom,TTo> : IValueConverter
    {
        public virtual TTo Convert(TFrom value, object parameter, CultureInfo culture)
        {
            return default(TTo);
        }
        public virtual TFrom ConvertBack(TTo value, object parameter, CultureInfo culture)
        {
            return default(TFrom);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TFrom)) return default(TTo);
            if (targetType != typeof(TTo)) return default(TTo);
            return Convert((TFrom)value, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TTo)) return default(TFrom);
            if (targetType != typeof(TFrom)) return default(TFrom);
            return ConvertBack((TTo)value, parameter, culture);
        }
    }
}

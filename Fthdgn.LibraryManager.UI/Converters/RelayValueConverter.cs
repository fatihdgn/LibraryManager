using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Converters
{
    public class RelayValueConverter<TFrom, TTo> : ValueConverter<TFrom, TTo>
    {
        Func<TFrom, object, CultureInfo, TTo> convert;
        Func<TTo, object, CultureInfo, TFrom> convertBack;
        public RelayValueConverter(Func<TFrom, object, CultureInfo, TTo> convert = null, Func<TTo, object, CultureInfo, TFrom> convertBack = null)
        {
            this.convert = convert;
            this.convertBack = convertBack;
        }
        public override TTo Convert(TFrom value, object parameter, CultureInfo culture) => convert != null ? convert(value, parameter, culture) : base.Convert(value, parameter, culture);
        public override TFrom ConvertBack(TTo value, object parameter, CultureInfo culture) => convertBack != null ? convertBack(value, parameter, culture) : base.ConvertBack(value, parameter, culture);
    }
}

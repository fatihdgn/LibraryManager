using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Converters
{
    public class NullableToDefaultConverter<T> : RelayValueConverter<T?, T>
        where T : struct
    {
        public NullableToDefaultConverter() : base((x, _, __) => x.HasValue ? x.Value : default(T), (x, _, __) => new T?(x)) { }
    }
}

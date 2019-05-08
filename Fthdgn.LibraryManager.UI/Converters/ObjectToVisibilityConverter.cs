using Fthdgn.LibraryManager.UI.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Fthdgn.LibraryManager.UI.Converters
{
    public class ObjectToVisibilityConverter : RelayValueConverter<object, Visibility>
    {
        public const char SplitCharacter = '|';
        public ObjectToVisibilityConverter() : base((o, param, _) => param?.ToString()?.SplitAndTrim(SplitCharacter).Contains(o?.ToString()) ?? false ? Visibility.Visible : Visibility.Collapsed) { }
    }
}

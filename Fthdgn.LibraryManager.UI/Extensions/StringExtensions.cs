using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitAndTrim(this string self, params char[] seperator)
        {
            return self?.Split(seperator, StringSplitOptions.None)?.Select(x => x?.Trim());
        }
    }
}

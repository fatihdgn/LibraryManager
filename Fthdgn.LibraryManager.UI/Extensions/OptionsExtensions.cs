using AutoMapper;
using Fthdgn.LibraryManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Extensions
{
    public static class OptionsExtensions
    {
        public static Options<TTo> MapTo<TFrom, TTo>(this Options<TFrom> self, Func<TFrom, TTo> mapper = null) => new Options<TTo>((mapper ?? Mapper.Map<TFrom, TTo>)(self.Value), self.IsViewable, self.IsEditable, self.IsDeletable, self.IsSelectable);
    }
}

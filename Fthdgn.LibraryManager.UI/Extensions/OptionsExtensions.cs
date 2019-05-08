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
        public static Options<TTo> MapTo<TFrom, TTo>(this Options<TFrom> self) => new Options<TTo>(Mapper.Map<TTo>(self.Value), self.IsViewable, self.IsEditable, self.IsDeletable, self.IsSelectable);
    }
}

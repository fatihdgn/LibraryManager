using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Extensions
{
    public static class FunctionalExtensions
    {
        public static T With<T>(this T self, Action<T> job)
        {
            job?.Invoke(self);
            return self;
        }

        public static TTo As<TFrom, TTo>(this TFrom self, Func<TFrom, TTo> job) => job == null ? default(TTo) : job(self);
    }
}

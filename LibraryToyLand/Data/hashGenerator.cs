using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data
{
    public class hashGenerator
    {
        public static ulong Hash(DateTime when)
        {
            ulong kind = (ulong)(int)when.Kind;
            return (kind << 62) | (ulong)when.Ticks;
        }
    }
}

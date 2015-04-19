using System;
using System.Collections.Generic;
using System.Linq;

namespace Sloccer.Core
{
    public static class Extensions
    {
        public static string ToLines(this IEnumerable<Line> source)
        {
            return string.Join(Environment.NewLine, source.Select(l => l.Text));
        }
    }
}

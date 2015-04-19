using System;
using System.Collections.Generic;
using System.Linq;

namespace Sloccer.Core
{
    public static class Extensions
    {
        public static string ConvertToString(this IEnumerable<Line> source)
        {
            return string.Join(Environment.NewLine, source.Select(l => l.Text));
        }

        public static List<string> ToLines(this string source)
        {
            return source.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }
    }
}

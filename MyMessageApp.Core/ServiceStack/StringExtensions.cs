using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageApp.Core.ServiceStack
{
    public static class StringExtensions
    {
        public static int ToInt(this string text) => text == null ? default(int) : int.Parse(text);

        public static int ToInt(this string text, int defaultValue) => int.TryParse(text, out var ret) ? ret : defaultValue;
    }
}

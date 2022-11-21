using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageApp.Core.Extensions
{
    public static class EnumerationExtensions
    {
      
        public static string GetDescription<T>(this T data)
        {
            FieldInfo fi = data.GetType().GetField(data.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return data.ToString();
        }

        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ParseEnum<T>(this byte value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}

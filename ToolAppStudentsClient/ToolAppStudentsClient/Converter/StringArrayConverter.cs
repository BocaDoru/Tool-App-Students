using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolAppStudentsClient.Converter
{
    public class StringArrayConverter:TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string[])||sourceType==typeof(ArrayExtension);
        }
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string[] stringArray)
            {
                return stringArray;
            }

            if (value is ArrayExtension arrayExtension)
            {
                if (arrayExtension.Type == typeof(string) && arrayExtension.Items != null)
                {
                    return arrayExtension.Items.Cast<string>().ToArray();
                }
            }

            return null;
        }
    }
}

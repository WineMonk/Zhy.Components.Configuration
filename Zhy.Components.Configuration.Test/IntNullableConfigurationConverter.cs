using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhy.Components.Configuration.Test
{
    /// <summary>
    /// 类型转换器
    /// </summary>
    public class IntNullableConfigurationConverter : IConfigurationConverter
    {
        public object Read(string value)
        {
            if (value == null)
            {
                return null;
            }
            int? valueAsInt = int.Parse(value);
            return valueAsInt;
        }

        public string Write(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return value + "";
        }
    }

}

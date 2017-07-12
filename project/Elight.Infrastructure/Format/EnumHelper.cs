using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Elight.Infrastructure
{
    /// <summary>
    /// 枚举类型操作公共类。
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举所有成员名称。
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        public static string[] GetNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }

        /// <summary>
        /// 检测枚举是否包含指定成员。
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="member">成员名或成员值</param>
        public static bool IsDefined(this Enum value)
        {
            Type type = value.GetType();
            return Enum.IsDefined(type, value);
        }

        /// <summary>
        /// 返回指定枚举类型的指定值的描述。
        /// </summary>
        /// <param name="t">枚举类型</param>
        /// <param name="v">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            try
            {
                Type type = value.GetType();
                FieldInfo field = type.GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }


    }
}

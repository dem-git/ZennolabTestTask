using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CaptchaApp.Server.Extentions
{
    /// <summary>
    /// Клссс со вспомогательными методами для перечислений.
    /// </summary>
    public static class EnumExtentions
    {
        /// <summary>
        /// Получить описание значения перечисления.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            var memberInfo = value.GetType().GetMember(value.ToString()).First();
            return memberInfo.GetCustomAttribute<DescriptionAttribute>().Description;
        }
    }
}

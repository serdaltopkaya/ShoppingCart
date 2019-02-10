using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public static partial class ShoppingCartExtensions
    {
        /// <summary>
        /// Checks and throws if  referance null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        internal static T ThrowIfNull<T>(this T o, string paramName) where T : class
        {
            if (o == null)
                throw new ArgumentException(paramName);
            else if(typeof(T).Equals(typeof(string)))
            {
                if(string.IsNullOrEmpty(o.ToString()))
                    throw new ArgumentException(paramName);
            }

            return o;
        }
    }
}

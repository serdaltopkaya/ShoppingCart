using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public static partial class ShoppingCartExtensions
    {
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

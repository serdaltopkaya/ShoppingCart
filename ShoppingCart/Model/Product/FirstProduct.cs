using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public class FirstProduct : ProductBase
    {
        public FirstProduct(int id, string title, decimal price, CategoryBase category) : base(id, title, price, category)
        {
        }
    }
}

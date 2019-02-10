using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerceShopping
{
    public class DeliverDynamic : DeliveryBase
    {
        public DeliverDynamic(decimal fixedPrice, decimal costPerDelivery, decimal costPerPrpduct) : base(fixedPrice, costPerDelivery, costPerPrpduct)
        {
        }

        public override decimal CalculateCost(ShoppingCart cart)
        {
            cart.ThrowIfNull(nameof(cart));

            DetectCategoryAndProductCount(cart._selectedProducts, out int categoryCount, out int productCount);

            var totalCost = _fixedPrice + (categoryCount * _costPerDelivery) + (productCount * _costPerProduct);

            return totalCost > 0 ? totalCost : 0;
        }

        private void DetectCategoryAndProductCount(List<ProductBase> products, out int categoryCount, out int productCount)
        {
            if(products.Count() == 0)
            {
                categoryCount = 0;
                productCount = 0;

                return;
            }

            categoryCount = products.GroupBy(x => x._category._id).Count();

            productCount = products.GroupBy(x=>  new { x._id, x._category  }).Count();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public abstract class DeliveryBase
    {
        private decimal FixedPrice;
        private decimal CostPerDelivery;
        private decimal CostPerProduct;        

        public DeliveryBase(decimal fixedPrice, decimal costPerDelivery, decimal costPerProduct)
        {
            if(fixedPrice < 0 || costPerDelivery < 0  || costPerProduct < 0)
                throw new ArgumentException();

            FixedPrice = fixedPrice;
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
        }
        public abstract decimal CalculateCost(ShoppingCart cart);

        public decimal _fixedPrice => FixedPrice;
        public decimal _costPerDelivery => CostPerDelivery;
        public decimal _costPerProduct => CostPerProduct;
    }
}

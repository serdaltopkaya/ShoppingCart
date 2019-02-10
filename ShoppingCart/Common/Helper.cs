using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerceShopping
{
    public static class Helper
    {
        /// <summary>
        /// Calculates sum price of product list
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public static decimal CalculateSumOfPrice(IList<ProductBase> products)
        {
            if (products.Count() > 0)
                return products.Sum(x => x._price);

            return 0;
        }

        public static List<CampaignBase> GetActiveCampaigns()
        {
            // Will be get from Database or chache
            return new List<CampaignBase>();
        }

        public static decimal CalculateCampaignDiscount(ShoppingCart cart)
        {
            cart.ThrowIfNull(nameof(cart));

            var activeCompaigns = Helper.GetActiveCampaigns();

            activeCompaigns.ForEach(x => { x.CalculateDiscount(cart._selectedProducts); });

            return activeCompaigns.Max(x => x._calculatedDiscountAmount);
        }
       
    }
}

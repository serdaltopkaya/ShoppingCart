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
        public static decimal CalculateSumPriceOfProductList(List<ProductBase> products)
        {
            products.ThrowIfNull(nameof(products));

            if (products.Count() > 0)
                return products.Sum(x => x._price);

            return 0;
        }

        public static List<CampaignBase> GetActiveCampaigns()
        {
            // Will be get from Database or chache
            return new List<CampaignBase>();
        }

        public static decimal CalculateCampaignDiscount(ShoppingCart cart, List<CampaignBase> campaigns)
        {
            if (campaigns != null && campaigns.Count() > 0 && cart != null && cart._selectedProducts.Count() > 0)
            {
                campaigns.ForEach(x => { x.CalculateDiscount(cart._selectedProducts); });

                return campaigns.Max(x => x._calculatedDiscountAmount);
            }

            return 0;
        }       
    }
}

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

        public static void CalculateCampaignDiscount(List<ProductBase> products, List<CampaignBase> campaigns)
        {
            if (campaigns != null && campaigns.Count() > 0 && products != null && products.Count() > 0)
            {
                campaigns.ForEach(x => { x.CalculateDiscount(products); });                
            }            
        }

        public static decimal CalculateCampaignTotalDiscount(List<CampaignBase> campaigns)
        {
            if (campaigns != null && campaigns.Count() > 0)
            {
                return campaigns.Sum(x => x._calculatedDiscountAmount);
            }

            return 0;
        }

        public static decimal CalculateCouponTotalDiscount(List<CouponBase> coupons)
        {
            if (coupons != null && coupons.Count() > 0)
            {
                return coupons.Sum(x => x._calculatedDiscountAmount);
            }

            return 0;
        }

        public static List<CampaignBase> GetApproopriateCampaigns(List<CampaignBase> campaigns)
        {
            if(campaigns != null || campaigns.Count() > 0)
            {
               var appliedCampaigns = campaigns.Where(x => x._calculatedDiscountAmount > 0);

                var campaignsHasMaxDiscountByCategory = from c in campaigns
                                      group c by c._category into ctg
                                      select ctg.OrderByDescending(c => c._calculatedDiscountAmount).FirstOrDefault();

                return campaignsHasMaxDiscountByCategory.ToList();
            }

            return new List<CampaignBase>();
        }
    }
}

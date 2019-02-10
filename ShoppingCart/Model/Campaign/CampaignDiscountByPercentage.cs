using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerceShopping
{
    public class CampaignDiscountByPercentage : CampaignBase
    {
        private readonly int TrasholdCount;
        public CampaignDiscountByPercentage(int id, string title, decimal discount, CategoryBase category, int trasholdCount) : base(id, title, discount, category)
        {
            TrasholdCount = trasholdCount;
        }

        /// <summary>
        /// Calculate campaign discount via appropriate product
        /// </summary>
        /// <param name="products"></param>
        public override void CalculateDiscount(List<ProductBase> products)
        {
            if (products != null && products.Count() > 0)
            {
                var campaignReltedProducts = products.Where(x => x._category._id == _category._id).ToList();

                if (campaignReltedProducts.Count >= TrasholdCount)
                {
                    CalculatedDiscountAmount = (Helper.CalculateSumPriceOfProductList(campaignReltedProducts) * Discount) / 100;                    
                }
            }
        }
    }
}

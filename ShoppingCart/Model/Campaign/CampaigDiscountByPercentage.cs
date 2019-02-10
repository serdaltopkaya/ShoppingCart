using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerceShopping
{
    public class CampaigDiscountByPercentage : CampaignBase
    {
        private readonly int TrasholdCount;
        public CampaigDiscountByPercentage(int id, string title, decimal discount, CategoryBase category, int trasholdCount) : base(id, title, discount, category)
        {
            TrasholdCount = trasholdCount;
        }

        /// <summary>
        /// checks campaign specific category products contain quantity then calculate discount amount.
        /// </summary>
        /// <param name="products"></param>
        public override void CalculateDiscount(List<ProductBase> products)
        {
            var campaignReltedProducts = products.Where(x => x._category._id == _category._id).ToList();

            if (products.Count >= TrasholdCount)
            {
                if(campaignReltedProducts.Count() > 0)
                {
                    CalculatedDiscountAmount = Helper.CalculateSumOfPrice(campaignReltedProducts) * Discount;
                }
            }
        }
    }
}

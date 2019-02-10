using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerceShopping
{
    public class CampaignDiscountByAmount : CampaignBase
    {
        private readonly decimal TrasholdCount;
        public CampaignDiscountByAmount(int id, string title, decimal discount, CategoryBase category, decimal trasholdCount) : base(id, title, discount, category)
        {
            TrasholdCount = trasholdCount;
        }

        public override void CalculateDiscount(List<ProductBase> products)
        {
            var campaignReltedProducts = products.Where(x => x._category._id == _category._id).ToList();

            if (products.Count() >= TrasholdCount)
            {
                if (campaignReltedProducts.Count() > 0)
                {
                    CalculatedDiscountAmount = campaignReltedProducts.Count() * Discount;
                }
            }
        }
    }
}

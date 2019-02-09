using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public abstract class CampaignBase
    {
        protected readonly int Id;
        protected readonly string Title;
        protected readonly decimal Discount;
        protected readonly CategoryBase Category;
        protected decimal CalculatedDiscountAmount = 0;

        public CampaignBase(int id, string title, decimal discount, CategoryBase category)
        {
            if (id <= 0 || discount < 0)
                throw new ArgumentException();

            Id = id;
            Discount = discount;
            Title = title.ThrowIfNull(nameof(title));            
            Category = category.ThrowIfNull(nameof(category));
        }

        public int _id => Id;
        public string _title => Title;
        public decimal _discount => Discount;
        public CategoryBase _category => Category;
        public decimal _calculatedDiscountAmount => CalculatedDiscountAmount;

        /// <summary>
        /// CalculatedDiscountAmount will be calculated
        /// </summary>
        public abstract void CalculateDiscount(List<ProductBase> products);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public abstract class ProductBase
    {
        private readonly int Id;
        private readonly string Title;
        private readonly decimal Price;
        private readonly CampaignBase Category;
        public ProductBase (int id, string title, decimal price, CampaignBase category)
        {            
            if(id <= 0 || price < 0 )
                throw new ArgumentException();

            Id = id;
            Price = price;
            Title = title.ThrowIfNull(nameof(title));
            Category = category.ThrowIfNull(nameof(category));
        }

        public int _id => Id;
        public string _title => Title;
        public decimal _price => Price;
        public CampaignBase _category => Category;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public abstract class CategoryBase
    {
        private readonly int Id;
        private readonly string Title;
        public CategoryBase(int id, string title)
        {
            if(id <= 0)
                throw new ArgumentException();

            Id = id;
            Title = title.ThrowIfNull(nameof(title));
        }

        public int _id => Id;
        public string _title => Title;
    }
}

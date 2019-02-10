using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public abstract class CouponBase
    {
        private readonly int CartId;
        protected bool IsUsedInCalculation;
        protected bool IsActive;
        protected readonly decimal TrasholdAmount;
        protected readonly decimal Discount;
        protected decimal CalculatedDiscountAmount = 0;

        public CouponBase(int cartId, decimal trasholdAmount, decimal discount)
        {
            if ( cartId <= 0 || trasholdAmount < 0 || discount < 0 )
                throw new ArgumentException();

            CartId = cartId;
            TrasholdAmount = trasholdAmount;
            Discount = discount;
            IsUsedInCalculation = false;
        }

        public decimal _calculatedDiscountAmount => CalculatedDiscountAmount;

        public abstract void CalculateApplicableCopons(decimal amount);
    }
}

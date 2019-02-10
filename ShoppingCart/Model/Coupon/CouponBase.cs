using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public abstract class CouponBase
    {
        public int CartId { get; set; }
        protected bool IsUsedInCalculation;
        protected bool IsActive;
        protected decimal TrasholdAmount;
        protected decimal Discount;
        protected decimal CalculatedDiscountAmount = 0;
        
        public decimal _discount => Discount;
        public decimal _trasholdAmount => TrasholdAmount;
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

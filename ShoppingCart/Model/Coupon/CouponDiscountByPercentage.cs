using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceShopping
{
    public class CouponDiscountByPercentage : CouponBase
    {
        public CouponDiscountByPercentage(int cartId, decimal trasholdAmount, decimal discount) : base(cartId, trasholdAmount, discount)
        {
        }

        /// <summary>
        /// Calculate applicable Copons
        /// </summary>
        /// <param name="amount"></param>
        public override void CalculateApplicableCopons(decimal amount)
        {
            if (!IsUsedInCalculation)
            {

                if (amount > TrasholdAmount)
                {
                    IsUsedInCalculation = true;
                    CalculatedDiscountAmount = amount * Discount/100;
                }
            }
        }
    }
}

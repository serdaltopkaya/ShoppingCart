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
        /// after checks coupon applicable if it is then apply copun an sign it to inactive alfter usege for prevent multi time usage
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

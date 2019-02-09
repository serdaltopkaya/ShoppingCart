using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerceShopping
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            SelectedProducts = new List<ProductBase>();
            CartCoupons = new List<CouponBase>();
        }
        private List<ProductBase> SelectedProducts;
        private List<CouponBase> CartCoupons;
        private bool IsCampaignApplied;
        private bool IsCouponApplied;
        private decimal CampaignTotalDiscount;
        private decimal CouponTotalDiscount;

        public decimal SumOfProducts { get { return Helper.CalculateSumOfPrice(SelectedProducts); } }
        public decimal SumAfterCampaign { get { return SumOfProducts >= CampaignTotalDiscount ? (SumOfProducts - CampaignTotalDiscount) : 0; } }
        public decimal SumAfterCoupon { get { return SumAfterCampaign >= CouponTotalDiscount ? (SumAfterCampaign - CouponTotalDiscount) : 0; } }

        public void AddCoupon(CouponBase coupon)
        {
            coupon.ThrowIfNull(nameof(coupon));
            CartCoupons.Add(coupon);
        }

        public void AddProduct(ProductBase product)
        {
            product.ThrowIfNull(nameof(product));
            SelectedProducts.Add(product);           
        }

        public void DeleteProduct(int productId, int count = 1)
        {
            var reletedItem = SelectedProducts.Where(x => x._id == productId).ToList();
            if (reletedItem.Count() > 0)
            {
                if (reletedItem.Count() > count)
                {
                    while (count > 0)
                    {
                        SelectedProducts.RemoveAt(SelectedProducts.FindLastIndex(x => x._id == productId));
                        count -= 1;
                    }
                }
                else
                {
                    SelectedProducts.RemoveAll(x => x._id == productId);
                }
            }
        }

        public void ApplyCompaigns()
        {
            if (!IsCampaignApplied)
            {
                ResetCouponUsage();

                CampaignTotalDiscount = Helper.CalculateCampaignDiscount(this);                

                IsCampaignApplied = true;
            }
        }

        public void ApplyCoupon()
        {            
            if (IsCampaignApplied && !IsCouponApplied)
            {
                CartCoupons.ForEach(x => { x.CalculateApplicableCopons(SumAfterCampaign); });
                IsCouponApplied = true;
                CouponTotalDiscount = 0;
            }
        }

        public decimal TotalySum()
        {
            return SumAfterCoupon;
        }

        private void ResetCouponUsage()
        {
            IsCouponApplied = false;
            CouponTotalDiscount = 0;
        }

        private void ResetCampaignUsage()
        {
            IsCampaignApplied = false;
            CampaignTotalDiscount = 0;
        }

        public void ApplyDiscounts()
        {
            ResetCouponUsage();
            ResetCampaignUsage();

            ApplyCompaigns();
            ApplyCoupon();
        }

        public List<ProductBase> _selectedProducts => SelectedProducts;
    }
}

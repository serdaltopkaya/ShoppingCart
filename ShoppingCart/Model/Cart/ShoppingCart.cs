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
        }
        private List<ProductBase> SelectedProducts;
        private List<CouponBase> CartCoupons;
        private bool IsCampaignApplied;
        private bool IsCouponApplied;
        private decimal CampaignTotalDiscount;
        private decimal CouponTotalDiscount;

        public List<ProductBase> _selectedProducts => SelectedProducts ?? new List<ProductBase>();
        public List<CouponBase> _cartCoupons => CartCoupons ?? new List<CouponBase>();

        public decimal SumOfProducts { get { return Helper.CalculateSumPriceOfProductList(_selectedProducts); } }
        public decimal SumAfterCampaign { get { return SumOfProducts >= CampaignTotalDiscount ? (SumOfProducts - CampaignTotalDiscount) : 0; } }
        public decimal SumAfterCoupon { get { return SumAfterCampaign >= CouponTotalDiscount ? (SumAfterCampaign - CouponTotalDiscount) : 0; } }

        public decimal TotalySum => SumAfterCoupon;

        public void AddCoupon(CouponBase coupon)
        {
            if (CartCoupons == null)
                CartCoupons = new List<CouponBase>();

            coupon.ThrowIfNull(nameof(coupon));
            CartCoupons.Add(coupon);
        }

        public void AddProduct(ProductBase product)
        {
            if (SelectedProducts == null)
                SelectedProducts = new List<ProductBase>();

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
                _cartCoupons.ForEach(x => { x.CalculateApplicableCopons(SumAfterCampaign); });
                IsCouponApplied = true;
                CouponTotalDiscount = 0;
            }
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
    }
}

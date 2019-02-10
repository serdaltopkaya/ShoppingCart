using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerceShopping
{
    public class ShoppingCart
    {
        public ShoppingCart(int id)
        {
            this.Id = id;
        }
        private int Id;
        private List<ProductBase> SelectedProducts;
        private List<CouponBase> CartCoupons;
        private bool IsCampaignApplied;
        private bool IsCouponApplied;
        private decimal CampaignMaxDiscount;
        private decimal CouponTotalDiscount;
        private decimal DeliveryCost;

        public List<ProductBase> _selectedProducts => SelectedProducts ?? new List<ProductBase>();
        public List<CouponBase> _cartCoupons => CartCoupons ?? new List<CouponBase>();

        public int _id => Id;
        public bool _isCampaignApplied => IsCampaignApplied;
        public bool _isCouponApplied => IsCouponApplied;
        public decimal _deliveryCost => DeliveryCost;

        public decimal _campaignMaxDiscount => CampaignMaxDiscount;
        public decimal _couponTotalDiscount => CouponTotalDiscount;

        public decimal SumOfProducts { get { return Helper.CalculateSumPriceOfProductList(_selectedProducts); } }
        public decimal SumAfterCampaign { get { return SumOfProducts >= CampaignMaxDiscount ? (SumOfProducts - CampaignMaxDiscount) : 0; } }
        public decimal TotalAmountAfterDiscount { get { return SumAfterCampaign >= CouponTotalDiscount ? (SumAfterCampaign - CouponTotalDiscount) : 0; } }
        public decimal TotalySumAffterDelivery => (TotalAmountAfterDiscount + DeliveryCost); 
 
        public void AddCoupon(CouponBase coupon)
        {
            if (CartCoupons == null)
                CartCoupons = new List<CouponBase>();

            coupon.ThrowIfNull(nameof(coupon));
            CartCoupons.Add(coupon);
            coupon.CartId = _id;
        }

        public void AddProduct(ProductBase product, int cout = 1)
        {
            if (SelectedProducts == null)
                SelectedProducts = new List<ProductBase>();

            product.ThrowIfNull(nameof(product));
            while (cout > 0)
            {
                SelectedProducts.Add(product);
                cout -= 1;
            }
            
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

        public void ApplyCampaigns(List<CampaignBase> campaigns)
        {
            if (!IsCampaignApplied  && !IsCouponApplied)
            {                
                CampaignMaxDiscount = Helper.CalculateCampaignDiscount(_selectedProducts, campaigns);

                IsCampaignApplied = true;
            }
        }

        public void ApplyCoupon()
        {
            if (!IsCouponApplied)
            {
                _cartCoupons.ForEach(x => { x.CalculateApplicableCopons(SumAfterCampaign); });
                IsCouponApplied = true;
                CouponTotalDiscount = 0;
            }
        }

        public void ApplayDelivery(DeliveryBase delivery)
        {
            delivery.ThrowIfNull(nameof(delivery));

            DeliveryCost =  delivery.CalculateCost(_selectedProducts);
        }
        
        private void ResetCouponUsage()
        {
            IsCouponApplied = false;
            CouponTotalDiscount = 0;
        }

        private void ResetCampaignUsage()
        {
            IsCampaignApplied = false;
            CampaignMaxDiscount = 0;
        }

        public void ApplyDiscounts(List<CampaignBase> campaigns)
        {
            ResetCouponUsage();
            ResetCampaignUsage();

            ApplyCampaigns(campaigns);
            ApplyCoupon();
        }
    }
}

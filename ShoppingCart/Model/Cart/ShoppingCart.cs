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
        private List<CampaignBase> AppliedCampaigns;
        private bool IsCampaignApplied;
        private bool IsCouponApplied;
        private decimal CampaignTotalDiscount;
        private decimal CouponTotalDiscount;
        private decimal DeliveryCost;

        public List<ProductBase> _selectedProducts => SelectedProducts ?? new List<ProductBase>();
        public List<CouponBase> _cartCoupons => CartCoupons ?? new List<CouponBase>();

        public int _id => Id;
        public bool _isCampaignApplied => IsCampaignApplied;
        public bool _isCouponApplied => IsCouponApplied;
        public decimal _deliveryCost => DeliveryCost;
        public List<CampaignBase> _appliedCampaigns => AppliedCampaigns;
        public decimal _campaignMaxDiscount => CampaignTotalDiscount;
        public decimal _couponTotalDiscount => CouponTotalDiscount;

        public decimal SumOfProducts { get { return Helper.CalculateSumPriceOfProductList(_selectedProducts); } }
        public decimal SumAfterCampaign { get { return SumOfProducts >= CampaignTotalDiscount ? (SumOfProducts - CampaignTotalDiscount) : 0; } }
        public decimal TotalAmountAfterDiscount { get { return SumAfterCampaign >= CouponTotalDiscount ? (SumAfterCampaign - CouponTotalDiscount) : 0; } }
        public decimal TotalySumAffterDelivery => (TotalAmountAfterDiscount + DeliveryCost);

        /// <summary>
        /// Add Coupon
        /// </summary>
        /// <param name="coupon"></param>
        public void AddCoupon(CouponBase coupon)
        {
            if (CartCoupons == null)
                CartCoupons = new List<CouponBase>();

            coupon.ThrowIfNull(nameof(coupon));
            CartCoupons.Add(coupon);
            coupon.CartId = _id;
        }

        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cout"></param>
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

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="count"></param>
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
                        ResetAllDiscount();
                    }
                }
                else
                {
                    SelectedProducts.RemoveAll(x => x._id == productId);
                    ResetAllDiscount();
                }
            }
        }

        /// <summary>
        /// Apply campaigns discount
        /// </summary>
        /// <param name="campaigns"></param>
        public void ApplyCampaigns(List<CampaignBase> campaigns)
        {
            if (!IsCampaignApplied  && !IsCouponApplied)
            {                
                Helper.CalculateCampaignDiscount(_selectedProducts, campaigns);

                AppliedCampaigns = Helper.GetApproopriateCampaigns(campaigns);

                CampaignTotalDiscount = Helper.CalculateCampaignTotalDiscount(AppliedCampaigns);

                IsCampaignApplied = true;
            }
        }

        /// <summary>
        /// Apply Coupon Discount
        /// </summary>
        public void ApplyCoupon()
        {
            if (!IsCouponApplied)
            {
                _cartCoupons.ForEach(x => { x.CalculateApplicableCopons(SumAfterCampaign); });
                IsCouponApplied = true;
                CouponTotalDiscount = Helper.CalculateCouponTotalDiscount(_cartCoupons);
            }
        }

        /// <summary>
        /// calculate delivery cost
        /// </summary>
        /// <param name="delivery"></param>
        public void ApplayDelivery(DeliveryBase delivery)
        {
            delivery.ThrowIfNull(nameof(delivery));

            DeliveryCost =  delivery.CalculateCost(_selectedProducts);
        }
        
        /// <summary>
        /// reset coupun usage (if prod deleted from list)
        /// </summary>
        private void ResetCouponUsage()
        {
            IsCouponApplied = false;
            CouponTotalDiscount = 0;
            _cartCoupons.ForEach(x => { x.ResetUsage(); });
        }

        /// <summary>
        /// reset campaign usage (if prod deleted from list)
        /// </summary>
        private void ResetCampaignUsage()
        {
            IsCampaignApplied = false;
            CampaignTotalDiscount = 0;
            AppliedCampaigns = new List<CampaignBase>();
        }

        /// <summary>
        /// Apply campaign and Coupon Discounts
        /// </summary>
        /// <param name="campaigns"></param>
        public void ApplyDiscounts(List<CampaignBase> campaigns)
        {
            ResetCouponUsage();
            ResetCampaignUsage();

            ApplyCampaigns(campaigns);
            ApplyCoupon();
        }

        /// <summary>
        ///if product removed oll calculated discounts most me recalculate
        /// </summary>
        private void ResetAllDiscount()
        {
            ResetCampaignUsage();
            ResetCouponUsage();
        }

    }
}

using ECommerceShopping;
using System;
using System.Collections.Generic;

namespace ECommerceShopingCartUsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var shoppingCart = new ShoppingCart(1);
            var category = new FirstCategory(1, "Food");
            var apple = new FirstProduct(1, "Apple", 100, category);
            var almonds = new FirstProduct(1, "Almonds", 150, category);
            shoppingCart.AddProduct(apple, 3);
            shoppingCart.AddProduct(almonds);

            var campaign1 = new CampaignDiscountByPercentage(1, "Food Campaign 1", 20, category, 3);
            var campaign2 = new CampaignDiscountByPercentage(2, "Food Campaign 2", 50, category, 5);
            var campaign3 = new CampaignDiscountByAmount(3, "Food Campaign 3", 5, category, 0);
            var campaigns = new List<CampaignBase>() { campaign1, campaign2, campaign3 };

            var coupon = new CouponDiscountByPercentage(1, 100, 10);
            shoppingCart.AddCoupon(coupon);

            var delivery = new DeliverDynamic((decimal)2.99, 5, 3);

            // Act

            shoppingCart.ApplyDiscounts(campaigns);

            shoppingCart.ApplayDelivery(delivery);
            Console.WriteLine("Indirim öncesi Tutar : " + shoppingCart.SumOfProducts);
            Console.WriteLine("Indirim sonrası Tutar : "+ shoppingCart.TotalAmountAfterDiscount);
            Console.WriteLine("Kullanılan Kopon : "+shoppingCart._couponTotalDiscount);
            Console.WriteLine("Kampanya indirimi : "+shoppingCart._campaignMaxDiscount);


            Console.WriteLine("Kargo maliyeti : " +shoppingCart._deliveryCost);
            Console.WriteLine("Toplam Maliyet Tutarı : "+shoppingCart.TotalySumAffterDelivery);

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ECommerceShopping.Test
{
    public class CartTests
    {
        [Fact]
        public void AddCoupon_With_Invalid_Value_Throw_ArgumentException()
        {
            // Arrange
            var test = new ShoppingCart(1);
            // Act


            // Assert
            Assert.Throws<ArgumentException>(() => test.AddCoupon(null));
        }

        [Fact]
        public void AddCoupon_ShouldWork()
        {
            // Arrange
            var test = new ShoppingCart(1);
            var coupon = new CouponDiscountByAmount(1, 1, 1);
            // Act
            test.AddCoupon(coupon);

            // Assert
            Assert.True( test._cartCoupons != null && test._cartCoupons.Contains(coupon));
        }

        [Fact]
        public void AddProduct_With_Invalid_Value_Throw_ArgumentException()
        {
            // Arrange
            var test = new ShoppingCart(1);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => test.AddProduct(null));
        }

        [Fact]
        public void AddProduct_ShouldWork()
        {
            // Arrange
            var test = new ShoppingCart(1);
            var category = new FirstCategory(1, "test");
            var product = new FirstProduct(1, "test", 1, category);
            // Act
            test.AddProduct(product);

            // Assert
            Assert.True(test._cartCoupons != null && test._selectedProducts.Contains(product));
        }

        [Fact]
        public void DeleteProduct_ShouldWork()
        {
            // Arrange
            var test = new ShoppingCart(1);
            var category = new FirstCategory(1, "test");
            var product = new FirstProduct(1, "test", 1, category);
            test.AddProduct(product);

            // Act
            test.DeleteProduct(1, 1);
            // Assert
            Assert.True(test._selectedProducts.Count == 0);
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(1, -1)]
        [InlineData(1, 0)]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        public void DeleteProduct_ShouldWork_Theory(int id, int count)
        {
            // Arrange
            var test = new ShoppingCart(1);
            var category = new FirstCategory(1, "test");
            var product = new FirstProduct(1, "test", 1, category);
            test.AddProduct(product);

            // Act
            test.DeleteProduct(id, count);
            // Assert
            Assert.True(test._selectedProducts.Count == 1 );
        }        

        [Fact]
        public void ApplyCampaigns_Should_Work()
        {
            // Arrange            
            var shoppingCart = new ShoppingCart(1);
            var category = new FirstCategory(1, "test");
            var product = new FirstProduct(1, "test", 100, category);
            var product2 = new FirstProduct(1, "test", 100, category);
            var product3 = new FirstProduct(1, "test", 100, category);
            shoppingCart.AddProduct(product);
            shoppingCart.AddProduct(product2);
            shoppingCart.AddProduct(product3);

            // Act
            shoppingCart.ApplyCampaigns(new List<CampaignBase>());

            // Assert
            Assert.True(shoppingCart._isCampaignApplied == true);
        }

        [Fact]
        public void AddCoupon_Should_Work()
        {
            // Arrange            
            var shoppingCart = new ShoppingCart(1);
            var category = new FirstCategory(1, "test");
            var product = new FirstProduct(1, "test", 100, category);
            var product2 = new FirstProduct(1, "test", 100, category);
            var product3 = new FirstProduct(1, "test", 100, category);
            shoppingCart.AddProduct(product);
            shoppingCart.AddProduct(product2);
            shoppingCart.AddProduct(product3);

            var coupon = new CouponDiscountByAmount(1, 100, 20);
            shoppingCart.AddCoupon(coupon);
            
            // Act
            shoppingCart.ApplyCoupon();
            // Assert
            Assert.True(shoppingCart._isCouponApplied == true);
        }



        [Fact]
        public void DeveryScenario_Should_Work()
        {
            // Arrange            
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
            // Assert
            Assert.True(shoppingCart._isCouponApplied == true && shoppingCart._isCampaignApplied == true);
        }
    }
}

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

            //var campaign = new CampaignDiscountByPercentage(1, "test", 10, category, 2);
            //var campaigns = new List<CampaignBase>() { campaign };
            // Act
            //var camaignDiscount = Helper.CalculateCampaignDiscount(shoppingCart, campaigns);
            shoppingCart.ApplyCampaigns();

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
            
            shoppingCart.ApplyCoupon();
            // Assert
            Assert.True(shoppingCart._isCouponApplied == true);
        }
    }
}

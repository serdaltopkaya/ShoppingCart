using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ECommerceShopping.Test
{
    public class HelperTests
    {
        [Fact]
        public void CalculateSumPriceOfProductList_Shold_Work_With_Empty_List()
        {
            // Arrange
            var products = new List<ProductBase>();

            // Act
            var test_Price = Helper.CalculateSumPriceOfProductList(products);

            // Assert
            Assert.True(test_Price == 0);
        }

        [Fact]
        public void CalculateSumPriceOfProductList_Shold_Work()
        {
            // Arrange
            var products = new List<ProductBase>();
            var category = new FirstCategory(1, "test");
            var product = new FirstProduct(1, "test", 10, category);

            products.Add(product);
            // Act
            var test_Price = Helper.CalculateSumPriceOfProductList(products);

            // Assert
            Assert.True(test_Price == 10);
        }        

        [Fact]
        public void CalculateSumPriceOfProductList_With_Invalid_Value_Throw_ArgumentException()
        {
            // Arrange            
            
            // Act
            
            // Assert
            Assert.Throws<ArgumentException>(() => Helper.CalculateSumPriceOfProductList(null));
        }

        [Fact]
        public void CalculateCampaignDiscount_Shold_Work()
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

            var campaign = new CampaignDiscountByPercentage(1, "test", 10, category, 2);
            var campaigns = new List<CampaignBase>() { campaign};
            // Act
            var camaignDiscount = Helper.CalculateCampaignDiscount(shoppingCart, campaigns);

            // Assert
            Assert.True(camaignDiscount == 30);
        }
    }
}

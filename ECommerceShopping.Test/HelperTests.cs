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
    }
}

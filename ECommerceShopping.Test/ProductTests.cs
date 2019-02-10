using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ECommerceShopping.Test
{
    public class ProductTests
    {
        [Theory]
        [InlineData(-1, null, -1)]
        [InlineData(-1, "", 1)]
        [InlineData(0, null, 1.0)]
        [InlineData(0, "", 0)]
        [InlineData(1, "", 1)]
        [InlineData(0, "test", 1)]
        [InlineData(1, "test", -1)]
        public void Create_Product_With_Invalid_Value_Throw_ArgumentException_Tehory(int id, string title, decimal price)
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new FirstProduct(id, title, price, category));
        }

        [Theory]
        [InlineData(1, "test", 1)]
        public void Create_Product_ShouldWork_Tehory(int id, string title, decimal price)
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            // Act
            var test = new FirstProduct(id, title, price, category);

            // Assert
            Assert.True(test != null && test._id == id && test._title.Equals(title));
        }
    }
}

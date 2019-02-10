using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ECommerceShopping.Test
{
    public class CouponTests
    {
        [Theory]
        [InlineData(-1, -1, -1)]
        [InlineData(-1, -1, 1)]
        [InlineData(0, 1.0, -1)]
        [InlineData(0, 0, -1)]
        [InlineData(1, 1, -1)]
        [InlineData(0, 1, -1)]
        [InlineData(1, -1, -1)]
        public void Create_CouponDiscountByPercentage_With_Invalid_Value_Throw_ArgumentException_Tehory(int id, decimal discount, int trashold)
        {
            // Arrange
            
            // Act
            Assert.Throws<ArgumentException>(() => new CouponDiscountByPercentage(id, trashold, discount));
            // Assert

        }

        [Theory]
        [InlineData(-1, 0, -1)]
        [InlineData(-1, -1, 1)]
        [InlineData(0, 1.0, -1)]
        [InlineData(0, 0, -1)]
        [InlineData(1, 1, -1)]
        [InlineData(0, 1, -1)]
        [InlineData(1, -1, -1)]
        public void Create_CouponDiscountByAmount_With_Invalid_Value_Throw_ArgumentException_Tehory(int id, decimal discount, int trashold)
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            // Act
            Assert.Throws<ArgumentException>(() => new CouponDiscountByAmount(id, trashold, discount));
            // Assert

        }
    }
}

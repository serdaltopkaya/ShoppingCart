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

            // Assert
            Assert.Throws<ArgumentException>(() => new CouponDiscountByPercentage(id, trashold, discount));
        }

        [Theory]
        [InlineData(1, 1, 1)]
        public void Create_CouponDiscountByPercentage__ShouldWork_Tehory(int id, decimal discount, int trashold)
        {
            // Arrange

            // Act
            var test = new CouponDiscountByPercentage(id, trashold, discount);
            // Assert
            Assert.True(test != null && test._cartId == id && test._discount == discount && test._trasholdAmount == trashold);
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

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new CouponDiscountByAmount(id, trashold, discount));
        }

        [Theory]
        [InlineData(1, 1, 1)]        
        public void Create_CouponDiscountByAmount__ShouldWork_Tehory(int id, decimal discount, int trashold)
        {
            // Arrange

            // Act
            var test = new CouponDiscountByAmount(id, trashold, discount);
            // Assert
            Assert.True(test != null && test._cartId == id && test._discount == discount && test._trasholdAmount == trashold);
        }
    }
}

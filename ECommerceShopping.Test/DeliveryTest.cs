using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ECommerceShopping.Test
{
    public class DeliveryTest
    {
        [Theory]
        [InlineData(-1, -1, -1)]
        [InlineData(-1, 1, -1)]
        [InlineData(0, 1.0, -1)]
        [InlineData(0, 0, -1)]
        [InlineData(1, 1, -1)]
        [InlineData(0, 1, -1)]
        [InlineData(1, -1, -1)]
        public void Create_Delivery_With_Invalid_Value_Throw_ArgumentException_Tehory(decimal fixedPrice, decimal costPerDelivery, decimal costPerProduct)
        {
            // Arrange
            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new DeliverDynamic(fixedPrice, costPerDelivery, costPerProduct));
        }

        [Theory]
        [InlineData(1, 1, 1)]
        public void Create_Deliverye_Should_Work_Tehory(decimal fixedPrice, decimal costPerDelivery, decimal costPerProduct)
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            // Act
            var test = new DeliverDynamic(fixedPrice, costPerDelivery, costPerProduct);
            // Assert
            Assert.True(test != null && test._fixedPrice == fixedPrice && test._costPerProduct == costPerProduct && test._costPerDelivery == costPerDelivery);
        }
    }
}

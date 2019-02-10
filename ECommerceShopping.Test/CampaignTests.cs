using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ECommerceShopping.Test
{
    public class CampaignTests
    {
        [Theory]
        [InlineData(-1, null, -1, -1)]
        [InlineData(-1, "", 1, -1)]
        [InlineData(0, null, 1.0, -1)]
        [InlineData(0, "", 0, -1)]
        [InlineData(1, "", 1, -1)]
        [InlineData(0, "test", 1, -1)]
        [InlineData(1, "test", -1, -1)]
        public void Create_CampaigDiscountByPercentage_With_Invalid_Value_Throw_ArgumentException_Tehory(int id, string title, decimal discount, int trashold)
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            // Act
            Assert.Throws<ArgumentException>(() => new CampaigDiscountByPercentage(id, title, discount, category, trashold));
            // Assert

        }

        [Theory]
        [InlineData(-1, null, -1, -1)]
        [InlineData(-1, "", 1, -1)]
        [InlineData(0, null, 1.0, -1)]
        [InlineData(0, "", 0, -1)]
        [InlineData(1, "", 1, -1)]
        [InlineData(0, "test", 1, -1)]
        [InlineData(1, "test", -1, -1)]
        public void Create_CampaigDiscountByAmount_With_Invalid_Value_Throw_ArgumentException_Tehory(int id, string title, decimal discount, int trashold)
        {                        
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            // Act
            Assert.Throws<ArgumentException>(() => new CampaignDiscountByAmount(id, title, discount, category, trashold));
            // Assert

        }
    }
}

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

            // Assert
            Assert.Throws<ArgumentException>(() => new CampaignDiscountByPercentage(id, title, discount, category, trashold));
        }

        [Theory]
        [InlineData(1, "test", 1, 1)]
        public void Create_CampaigDiscountByPercentage_Should_Work_Tehory(int id, string title, decimal discount, int trashold)
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            // Act
            var test = new CampaignDiscountByPercentage(id, title, discount, category, trashold);
            // Assert
            Assert.True(test !=null && test._id == id && test._title.Equals(title) &&  test._category == category);
        }


        [Fact]
        public void CalculateDiscount_For_CampaigDiscountByPercentage_Should_Work()
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            var test = new CampaignDiscountByPercentage(1, "test", 1, category, 3);                           

            // Act
            test.CalculateDiscount(null);

            // Assert
            Assert.True(test._calculatedDiscountAmount == 0);
        }


        [Fact]
        public void CalculateDiscount_For_CampaigDiscountByPercentage_Should_Work_AndApply_Discount()
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");

            var products = new List<ProductBase>();
            products.Add(new FirstProduct(1, "test", 10, category));
            products.Add(new FirstProduct(1, "test", 10, category));
            products.Add(new FirstProduct(1, "test", 10, category));

            var test = new CampaignDiscountByPercentage(1, "test", 10, category, 3);

            // Act
            test.CalculateDiscount(products);

            // Assert
            Assert.True(test._calculatedDiscountAmount == 3);
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

            // Assert
            Assert.Throws<ArgumentException>(() => new CampaignDiscountByAmount(id, title, discount, category, trashold));
        }

        [Theory]
        [InlineData(1, "test", 1, 1)]
        public void Create_CampaigDiscountByAmount_Should_Work_Tehory(int id, string title, decimal discount, int trashold)
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            // Act
            var test = new CampaignDiscountByAmount(id, title, discount, category, trashold);
            // Assert
            Assert.True(test != null && test._id == id && test._title.Equals(title) && test._category == category);
        }

        [Fact]
        public void CalculateDiscount_For_CampaigDiscountByAmount_Should_Work()
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");
            var test = new CampaignDiscountByAmount(1, "test", 1, category, 3);

            // Act
            test.CalculateDiscount(null);

            // Assert
            Assert.True(test._calculatedDiscountAmount == 0);
        }


        [Fact]
        public void CalculateDiscount_For_CampaigDiscountByAmount_Should_Work_AndApply_Discount()
        {
            // Arrange
            CategoryBase category = new FirstCategory(1, "First");

            var products = new List<ProductBase>();
            products.Add(new FirstProduct(1, "test", 10, category));
            products.Add(new FirstProduct(1, "test", 10, category));
            products.Add(new FirstProduct(1, "test", 10, category));

            var test = new CampaignDiscountByAmount(1, "test", 5, category, 3);

            // Act
            test.CalculateDiscount(products);

            // Assert
            Assert.True(test._calculatedDiscountAmount == 15);
        }

    }
}
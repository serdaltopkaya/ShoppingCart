using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ECommerceShopping.Test
{

    public class CategoryTests
    {
        [Theory]
        [InlineData(-1, null)]
        [InlineData(-1, "")]
        [InlineData(0, null)]
        [InlineData(0, "")]
        [InlineData(1, "")]
        [InlineData(0, "test")]
        public void Create_Category_With_Invalid_Value_Throw_ArgumentException_Tehory( int id, string title)
        {
            // Arrange

            // Act
            Assert.Throws<ArgumentException>(() => new FirstCategory(id, title));
            // Assert
            
        }

        [Theory]
        [InlineData(1, "test")]
        public void Create_Category_ShouldWork_Tehory(int id, string title)
        {
            // Arrange

            // Act
            var test = new FirstCategory(id, title);

            // Assert
            Assert.True(test != null && test._id == id && test._title.Equals(title));
        }
    }
}

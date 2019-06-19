using Xunit;
using FACP.PasswordStrength.Service.Helpers;

namespace FACP.PasswordStrength.Tests.Extensions
{
    public class StringExtensionTest
    {        
        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("abc", false)]
        public void StringExtension_IsNull(string input, bool expected)
        {
            var result = input.IsNull();

            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(null, 40, "")]
        [InlineData("1234567890", 2, "12")]
        [InlineData("1234567890", 3, "123")]
        [InlineData("1234567890", 4, "1234")]
        [InlineData("1234567890", 5, "12345")]
        public void StringExtension_Prefix(string input, int length, string expected)
        {
            var result = input.Prefix(length);

            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(null, 40, "")]
        [InlineData("1234567890", 2, "34567890")]
        [InlineData("1234567890", 3, "4567890")]
        [InlineData("1234567890", 4, "567890")]
        [InlineData("1234567890", 5, "67890")]
        public void StringExtension_Suffix(string input, int start, string expected)
        {
            var result = input.Suffix(start);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1234", null)]
        [InlineData("004B12E3EE8CEF2900B74A74C8EA3953059:1", "1")]
        [InlineData("0065BF65AC920FF37A2B98790A01BF144AB:3", "3")]
        [InlineData("2BAE07BEDC4C163F679A746F7AB7FB5D1FA:1505", "1505")]
        [InlineData("2C1AB4324F0C7080715E073A6553CAB0B47:15", "15")]
        public void StringExtension_NumberOfTimesAppearedInDataBreaches(string input, string expected)
        {
            var result = input.NumberOfTimesAppearedInDataBreaches();

            Assert.Equal(expected, result);
        }
    }
}

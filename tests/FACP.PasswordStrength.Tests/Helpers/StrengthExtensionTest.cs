using Xunit;
using FACP.PasswordStrength.Service.Enums;
using FACP.PasswordStrength.Service.Helpers;

namespace FACP.PasswordStrength.Tests.Extensions
{
    public class StrengthExtensionTest
    {
        [Theory]
        [InlineData("test", Strength.VeryWeak)]
        [InlineData("Test", Strength.VeryWeak)]        
        [InlineData("b1!<+", Strength.Weak)]
        [InlineData("ABc!Test", Strength.Weak)]        
        [InlineData("adf146!@#", Strength.Reasonable)]
        [InlineData("Bac086>", Strength.Reasonable)]
        [InlineData("AFbd13!# <>", Strength.Strong)]
        [InlineData("/Exp0rT3+> .", Strength.Strong)]
        [InlineData("AAFbd13!# <> é", Strength.VeryStrong)]
        [InlineData("/Exp0rTè+><3 ç", Strength.VeryStrong)]
        public void StrengthExtensio_PasswordStrength_Returns_Strength(string input, Strength expected)
        {
            var result = input.PasswordStrength();

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("a", 1)]
        [InlineData("test", 2)]
        [InlineData("Test", 3)]
        [InlineData("Test123", 4)]
        [InlineData("P@ssword!", 5)]
        [InlineData("@dM1n!@#", 6)]
        [InlineData("T3$tN<> ", 7)]
        [InlineData("p2$$w0eD <", 8)]
        [InlineData("AFbd1!# <>", 10)]
        [InlineData("/Exp0rT3+> .", 11)]
        [InlineData("AAFbd13!# <> é", 12)]
        public void StrengthExtensio_GetStrengthScore_Returns_Score(string input, int expected)
        {
            var result = input.GetStrengthScore();

            Assert.Equal(expected, result);
        }
    }
}

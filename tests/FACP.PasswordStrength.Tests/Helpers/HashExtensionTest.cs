using Xunit;
using FACP.PasswordStrength.Service.Helpers;

namespace FACP.PasswordStrength.Tests.Extensions
{
    public class HashExtensionTest
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "da39a3ee5e6b4b0d3255bfef95601890afd80709")]
        [InlineData("test", "a94a8fe5ccb19ba61c4c0873d391e987982fbbd3")]
        [InlineData("Password!", "ef8420d70dd7676e04bea55f405fa39b022a90c8")]
        [InlineData("Admin123", "7af2d10b73ab7cd8f603937f7697cb5fe432c7ff")]
        public void HashExtension_ToSHA1Hash(string input, string expected)
        {
            var result = input.ToSHA1Hash();

            Assert.Equal(expected, result);
        }
    }
}

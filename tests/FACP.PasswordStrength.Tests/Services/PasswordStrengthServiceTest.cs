using Moq;
using Xunit;
using FACP.PasswordStrength.Service.Implementations;
using FACP.PasswordStrength.Service.Interfaces;
using FACP.PasswordStrength.Service.Enums;

namespace FACP.PasswordStrength.Tests
{
    public class PasswordStrengthServiceTest
    {
        private readonly PasswordStrengthService _service;
        private Mock<IPwnedPasswordService> _moqPwnedPasswordService;

        public PasswordStrengthServiceTest()
        {
            _moqPwnedPasswordService = new Mock<IPwnedPasswordService>();
            _moqPwnedPasswordService.Setup(s => s.GetBreaches(It.IsAny<string>())).Returns("0");

            _service = new PasswordStrengthService(_moqPwnedPasswordService.Object);
        }

        [Theory]
        [InlineData("test", Strength.VeryWeak)]
        [InlineData("Test", Strength.VeryWeak)]
        [InlineData("123456", Strength.VeryWeak)]
        [InlineData("test123", Strength.VeryWeak)]
        [InlineData("admin", Strength.VeryWeak)]
        [InlineData("Admin", Strength.VeryWeak)]
        public void PasswordStrength_GetStrength_Returns_VeryWeak(string input, Strength expected)
        {
            var result = _service.GetStrength(input);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("b1!<+", Strength.Weak)]
        [InlineData("ABc!Test", Strength.Weak)]
        [InlineData("CvdAdmin123", Strength.Weak)]
        [InlineData("Admin123!", Strength.Weak)]
        [InlineData("ABC123!#a", Strength.Weak)]
        [InlineData("P@ssword!", Strength.Weak)]
        public void PasswordStrength_GetStrength_Returns_Weak(string input, Strength expected)
        {
            var result = _service.GetStrength(input);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("adf146!@#", Strength.Reasonable)]
        [InlineData("Bac086>", Strength.Reasonable)]
        [InlineData("R@nD0n!#m1c", Strength.Reasonable)]
        [InlineData("@dM1n!@#", Strength.Reasonable)]
        [InlineData("T3$tN<> ", Strength.Reasonable)]
        [InlineData("p2$$w0eD <", Strength.Reasonable)]
        public void PasswordStrength_GetStrength_Returns_Reasonable(string input, Strength expected)
        {
            var result = _service.GetStrength(input);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("AFbd13!# <>", Strength.Strong)]
        [InlineData("/Exp0rT3+> .", Strength.Strong)]
        [InlineData(" F1n@nC!Al$Y3ar+", Strength.Strong)]
        [InlineData("=$M@rk3T!nG 0>", Strength.Strong)]
        [InlineData("#1nFr@M()n3y ><", Strength.Strong)]
        [InlineData(" P@a$$w0rD=/3?", Strength.Strong)]
        public void PasswordStrength_GetStrength_Returns_Strong(string input, Strength expected)
        {
            var result = _service.GetStrength(input);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("AAFbd13!# <> é", Strength.VeryStrong)]
        [InlineData("/Exp0rTè+><3 ç", Strength.VeryStrong)]
        [InlineData(" F1n@nC!ál$Y3r+", Strength.VeryStrong)]
        [InlineData("=$M@rk3T!nG 0> ¿", Strength.VeryStrong)]
        [InlineData("#1nFr@M()n3y ><µ", Strength.VeryStrong)]
        [InlineData(" P@a$$w0rD=/3? Ã", Strength.VeryStrong)]
        public void PasswordStrength_GetStrength_Returns_VeryStrong(string input, Strength expected)
        {
            var result = _service.GetStrength(input);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("Test", "1505")]
        [InlineData(" P@a$$w0rD=/3? Ã", null)]
        public void PasswordStrength_NumberOfTimesAppearedInDataBreaches(string input, string expected)
        {
            _moqPwnedPasswordService.Setup(s => s.GetBreaches(It.IsAny<string>())).Returns(GetPwnedPasswordResponse());

            var result = _service.NumberOfTimesAppearedInDataBreaches(input);

            Assert.Equal(result, expected);
        }

        private string GetPwnedPasswordResponse()
        {
            return "004B12E3EE8CEF2900B74A74C8EA3953059:1\r\n" +
                "0065BF65AC920FF37A2B98790A01BF144AB:3\r\n" +
                "0078EE610A3FCCDE85560F1937A1E3C52B5:1\r\n" +
                "01E66F772E7B7BA7A1A1454B65E0ACEEF87:1\r\n" +
                "038E53E72EEB26BB4D54A0DB40C4C6826FE:2\r\n" +
                "03BFF1564EA5A688949C02692F6740379F2:2\r\n"+
                "2BAE07BEDC4C163F679A746F7AB7FB5D1FA:1505\r\n" +
                "2C1AB4324F0C7080715E073A6553CAB0B47:15\r\n" +
                "2D2D6A9364266A853F0440F9DE4C848C772:16";
        }

    }
}

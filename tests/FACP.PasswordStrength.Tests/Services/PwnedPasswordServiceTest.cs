using Moq;
using Xunit;
using FACP.PasswordStrength.Service.Interfaces;
using FACP.PasswordStrength.Service.Implementations;

namespace FACP.PasswordStrength.Tests.Services
{
    public class PwnedPasswordServiceTest
    {
        private PwnedPasswordService _service;
        private Mock<IRestExternalService> _restExternalService;

        public const string Data = "004B12E3EE8CEF2900B74A74C8EA3953059:1\r\n" +
                "0065BF65AC920FF37A2B98790A01BF144AB:3\r\n" +
                "0078EE610A3FCCDE85560F1937A1E3C52B5:1\r\n" +
                "01E66F772E7B7BA7A1A1454B65E0ACEEF87:1\r\n" +
                "038E53E72EEB26BB4D54A0DB40C4C6826FE:2\r\n" +
                "03BFF1564EA5A688949C02692F6740379F2:2\r\n" +
                "2BAE07BEDC4C163F679A746F7AB7FB5D1FA:1505\r\n" +
                "2C1AB4324F0C7080715E073A6553CAB0B47:15\r\n" +
                "2D2D6A9364266A853F0440F9DE4C848C772:16";

        public PwnedPasswordServiceTest()
        {
            _restExternalService = new Mock<IRestExternalService>();
            _service = new PwnedPasswordService(_restExternalService.Object);
        }

        [Theory]
        [InlineData("Test", Data)]
        public void PasswordStrength_NumberOfTimesAppearedInDataBreaches_Returns_Breaches(string input, string expected)
        {
            _restExternalService.Setup(s => s.Get(It.IsAny<string>())).Returns(Data);
            var result = _service.GetBreaches(input);

            Assert.Equal(expected, result);
        }
    }
}

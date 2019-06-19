using Xunit;
using Moq;
using RestSharp;

using FACP.PasswordStrength.Service.Implementations;
using System;

namespace FACP.PasswordStrength.Tests.Services
{
    public class RestExternalServiceTest
    {
        private RestExternalService _service;
        private Mock<IRestClient> _client;
        private RestResponse _restResponse;

        public RestExternalServiceTest()
        {
            _client = new Mock<IRestClient>();
            _service = new RestExternalService(_client.Object);
        }

        [Theory]
        [InlineData("someUrl", "Ok")]
        public void RestExternalService_Get_Returns_Ok(string input, string expected)
        {
            _restResponse = new RestResponse();
            _restResponse.StatusCode = System.Net.HttpStatusCode.OK;
            _restResponse.Content = "Ok";
            _restResponse.ErrorException = null;

            _client.Setup(s => s.Execute(It.IsAny<IRestRequest>())).Returns(_restResponse);

            var result = _service.Get(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("someUrl")]
        public void RestExternalService_Get_Returns_ThrowsException(string input)
        {
            _restResponse = new RestResponse();
            _restResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            _restResponse.Content = "Ok";
            _restResponse.ErrorException = null;

            _client.Setup(s => s.Execute(It.IsAny<IRestRequest>())).Returns(_restResponse);

            Assert.Throws<Exception>(() => _service.Get(input));
        }
    }
}

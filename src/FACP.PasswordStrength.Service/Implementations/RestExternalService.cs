using System;
using System.Net;
using RestSharp;

using FACP.PasswordStrength.Service.Interfaces;

namespace FACP.PasswordStrength.Service.Implementations
{
    public class RestExternalService : IRestExternalService
    {
        #region Fields

        private IRestClient _restClient;

        #endregion

        #region Constructor

        public RestExternalService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        #endregion

        #region Public Methods

        public string Get(string resource)
        {
            var request = new RestRequest(resource, Method.GET);
            return SendRequest(request);
        }

        #endregion

        #region Private Methods

        private string SendRequest(IRestRequest request)
        {
            request.RequestFormat = DataFormat.Json;

            var response = _restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception(response.Content);

            if (response.ErrorException != null)
                throw response.ErrorException;

            return response.Content;
        }

        #endregion
    }
}

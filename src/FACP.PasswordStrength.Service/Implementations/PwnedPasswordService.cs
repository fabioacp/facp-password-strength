using FACP.PasswordStrength.Service.Interfaces;

namespace FACP.PasswordStrength.Service.Implementations
{
    public class PwnedPasswordService : IPwnedPasswordService
    {
        private IRestExternalService _restExternalService;
        private string _url = "https://api.pwnedpasswords.com/range/";

        public PwnedPasswordService(IRestExternalService restExternalService)
        {
            _restExternalService = restExternalService;
        }

        public string GetBreaches(string hashPassword)
        {
            return _restExternalService.Get(_url + hashPassword);
        }

    }
}

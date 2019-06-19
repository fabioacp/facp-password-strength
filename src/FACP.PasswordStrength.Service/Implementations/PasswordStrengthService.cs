using System;
using System.Linq;
using FACP.PasswordStrength.Service.Enums;
using FACP.PasswordStrength.Service.Helpers;
using FACP.PasswordStrength.Service.Interfaces;

namespace FACP.PasswordStrength.Service.Implementations
{
    public class PasswordStrengthService : IPasswordStrengthService
    {
        private readonly IPwnedPasswordService _pwnedPasswordService;
        private const int _prefixAndSuffixIndexes = 5;

        public PasswordStrengthService(IPwnedPasswordService pwnedPasswordService)
        {
            _pwnedPasswordService = pwnedPasswordService;
        }

        public Strength GetStrength(string password)
        {
            return password.PasswordStrength();
        }

        public string NumberOfTimesAppearedInDataBreaches(string password)
        {
            if (password.IsNull())
                return string.Empty;

            var hashPassowrd = password.ToSHA1Hash();
            var prefix = hashPassowrd.Prefix(_prefixAndSuffixIndexes);
            var suffix = hashPassowrd.Suffix(_prefixAndSuffixIndexes);

            var pwnedPasswordService = _pwnedPasswordService.GetBreaches(prefix);

            var breaches = pwnedPasswordService.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var breached = breaches.FirstOrDefault(f => f.ToUpper().Contains(suffix.ToUpper()));

            return breached.NumberOfTimesAppearedInDataBreaches();
        }
    }
}

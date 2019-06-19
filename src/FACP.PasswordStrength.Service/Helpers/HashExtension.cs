using System.Security.Cryptography;
using System.Text;

namespace FACP.PasswordStrength.Service.Helpers
{
    public static class HashExtension
    {
        public static string ToSHA1Hash(this string input)
        {
            if (input == null)
                return null;

            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder(hash.Length * 2);

                foreach (byte item in hash)
                {
                    builder.Append(item.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}

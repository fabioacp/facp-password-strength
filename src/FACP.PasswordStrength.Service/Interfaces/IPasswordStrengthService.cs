using FACP.PasswordStrength.Service.Enums;

namespace FACP.PasswordStrength.Service.Interfaces
{
    public interface IPasswordStrengthService
    {
        Strength GetStrength(string password);

        string NumberOfTimesAppearedInDataBreaches(string password);

    }
}

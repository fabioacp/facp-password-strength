namespace FACP.PasswordStrength.Service.Interfaces
{
    public interface IPwnedPasswordService
    {
        string GetBreaches(string hashPassword);
    }
}

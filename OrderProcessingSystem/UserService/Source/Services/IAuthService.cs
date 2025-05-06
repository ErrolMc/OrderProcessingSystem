using OPS.UserService.Data;

namespace OPS.UserService.Services
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string hashedPassword);
    }
}
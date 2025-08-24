using OPS.UserService.Data;

namespace OPS.UserService.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByIdAsync(string id);
        public Task<User> GetUserByUsernameAsync(string username);
        public Task CreateUserAsync(User user);
        public Task<bool> DeleteUserAsync(string id);
    }
}


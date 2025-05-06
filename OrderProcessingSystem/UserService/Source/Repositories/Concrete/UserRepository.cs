using OPS.UserService.Data;

namespace OPS.UserService.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
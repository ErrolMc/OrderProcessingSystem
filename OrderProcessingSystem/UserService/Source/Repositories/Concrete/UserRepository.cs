using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OPS.Shared;
using OPS.UserService.Data;

namespace OPS.UserService.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptions<MongoDbSettings> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _users.Find(u => u.ID == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                await _users.InsertOneAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            try
            {
                DeleteResult res = await _users.DeleteOneAsync(u => u.ID == id);
                return res.DeletedCount == 1;
            }
            catch
            {
                return false;
            }
        }
    }
}

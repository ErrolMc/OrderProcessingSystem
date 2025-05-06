using OPS.Shared;

namespace OPS.UserService.Data
{
    public class User
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }

        public UserDto ToUserDto()
        {
            return new UserDto()
            {
                ID = ID,
                Username = Username,
                Address = Address
            };
        }
    }
}

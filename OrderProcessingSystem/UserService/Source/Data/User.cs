using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OPS.Shared.DTOs;

namespace OPS.UserService.Data
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string ID { get; set; }
        
        [BsonElement("username")]
        public string Username { get; set; }
        
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }
        
        [BsonElement("address")]
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

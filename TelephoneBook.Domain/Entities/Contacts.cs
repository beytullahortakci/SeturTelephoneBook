using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TelephoneBook.Domain.Entities
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactCompany { get; set; }
    }
}

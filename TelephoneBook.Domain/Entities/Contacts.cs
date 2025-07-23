using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TelephoneBook.Domain.Common;

namespace TelephoneBook.Domain.Entities
{
    public class Contact:IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactCompany { get; set; }
    }
}

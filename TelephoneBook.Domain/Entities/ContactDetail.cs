using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TelephoneBook.Domain.Enums;

namespace TelephoneBook.Domain.Entities
{
    public class ContactDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactId { get; set; }
        public ContactDetailsType ContactDetailsType { get; set; }
        public string Value { get; set; }
    }
}

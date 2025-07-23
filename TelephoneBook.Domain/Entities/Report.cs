using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Domain.Common;
using TelephoneBook.Domain.Enums;

namespace TelephoneBook.Domain.Entities
{
    public class Report:IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public ReportStatus Status { get; set; }

        public ReportDetail? ReportDetail { get; set; }
    }

    public class ReportDetail
    {
        public string Location { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int ContactCount { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int PhoneNumberCount { get; set; }
    }
}

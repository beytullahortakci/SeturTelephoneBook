using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Domain.Common
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public interface IBaseEntity
    {
     string Id { get; set; }
    }
}

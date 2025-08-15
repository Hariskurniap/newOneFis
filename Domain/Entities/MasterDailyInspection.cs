using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MasterDailyInspection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("groupName")]
        public string GroupName { get; set; }

        [BsonElement("mandatory")]
        public bool Mandatory { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("nameChoosen")]
        public string NameChoosen { get; set; }
    }
}

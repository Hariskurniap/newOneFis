using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class DailyInspection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("expiredDateTime")]
        public string ExpiredDateTime { get; set; }

        [BsonElement("inspectionBy")]
        public string InspectionBy { get; set; }

        [BsonElement("inspectionDateTime")]
        public string InspectionDateTime { get; set; }

        [BsonElement("planCode")]
        public string PlanCode { get; set; }

        [BsonElement("resultInspection")]
        public string ResultInspection { get; set; }

        [BsonElement("resultInspectionNotes")]
        public string ResultInspectionNotes { get; set; }

        [BsonElement("vehicleNumber")]
        public string VehicleNumber { get; set; }

        [BsonElement("componentInspection")]
        public List<ComponentInspection> ComponentInspection { get; set; }
    }

    public class ComponentInspection
    {
        [BsonElement("groupName")]
        public string GroupName { get; set; }

        [BsonElement("detail")]
        public Dictionary<string, string> Detail { get; set; }
    }
}

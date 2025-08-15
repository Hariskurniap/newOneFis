using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ShipmentValidations
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("shipmentCode")]
        public string ShipmentCode { get; set; }

        [BsonElement("plantCode")]
        public string PlantCode { get; set; }

        [BsonElement("employeeId")]
        public string EmployeeId { get; set; }

        [BsonElement("employeeName")]
        public string EmployeeName { get; set; }

        [BsonElement("employeePosition")]
        public string EmployeePosition { get; set; }

        [BsonElement("isValid")]
        public string IsValid { get; set; }

        [BsonElement("info")]
        public string Info { get; set; }

        [BsonElement("createdAt")]
        public string CreatedAt { get; set; }  // string karena di MongoDB format "yyyy-MM-dd HH:mm:ss"

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }
    }
}

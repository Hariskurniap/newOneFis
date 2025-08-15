using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class DcuCheckin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("dcuEmployeeId")]
        public string DcuEmployeeId { get; set; }

        [BsonElement("dcuName")]
        public string DcuName { get; set; }

        [BsonElement("amtEmployeeId")]
        public string AmtEmployeeId { get; set; }

        [BsonElement("amtEmployeeName")]
        public string AmtEmployeeName { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("result")]
        public string Result { get; set; }

        [BsonElement("bodyTemperature")]
        public string BodyTemperature { get; set; }

        [BsonElement("bloodPressure")]
        public string BloodPressure { get; set; }

        [BsonElement("oxygenLevel")]
        public string OxygenLevel { get; set; }

        [BsonElement("information")]
        public string Information { get; set; }

        [BsonElement("bloodSugar")]
        public string BloodSugar { get; set; }

        [BsonElement("dcuDate")]
        public string DcuDate { get; set; }

        [BsonElement("createdAt")]
        public string CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public string UpdatedAt { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonElement("plantCode")]
        public string PlantCode { get; set; }
    }
}

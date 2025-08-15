using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ShipmentChecklistActivities
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

        [BsonElement("deliveryNumber")]
        public string DeliveryNumber { get; set; }

        [BsonElement("stage")]
        public string Stage { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("location")]
        public List<LocationData> Location { get; set; }

        [BsonElement("checklistAt")]
        public string ChecklistAt { get; set; }

        [BsonElement("checklistBy")]
        public string ChecklistBy { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("checklistItems")]
        public List<ChecklistItem> ChecklistItems { get; set; }

        [BsonElement("note")]
        public string Note { get; set; }
    }

    public class LocationData
    {
        [BsonElement("latitude")]
        public string Latitude { get; set; }

        [BsonElement("longitude")]
        public string Longitude { get; set; }
    }

    public class ChecklistItem
    {
        [BsonElement("checked")]
        public bool Checked { get; set; }

        [BsonElement("itemName")]
        public string ItemName { get; set; }
    }
}

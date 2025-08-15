using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class CheckinOnefis
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("attendaceCode")]
        public string AttendaceCode { get; set; }

        [BsonElement("plantCode")]
        public string PlantCode { get; set; }

        [BsonElement("checkinAllocations")]
        public List<CheckinAllocation> CheckinAllocations { get; set; } = new List<CheckinAllocation>();

        [BsonElement("createdAt")]
        public string CreatedAt { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("updatedAt")]
        public string UpdatedAt { get; set; }

        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }
    }

    public class CheckinAllocation
    {
        [BsonElement("allocationId")]
        public string AllocationId { get; set; }

        [BsonElement("amtEmployeeId")]
        public string AmtEmployeeId { get; set; }

        [BsonElement("amtEmployeeName")]
        public string AmtEmployeeName { get; set; }

        [BsonElement("amtEmployeeRole")]
        public string AmtEmployeeRole { get; set; }

        [BsonElement("checkinDate")]
        public string CheckinDate { get; set; }

        [BsonElement("checkinLatitude")]
        public double CheckinLatitude { get; set; }

        [BsonElement("checkinLongitude")]
        public double CheckinLongitude { get; set; }

        [BsonElement("checkoutDate")]
        public string CheckoutDate { get; set; }

        [BsonElement("checkoutLatitude")]
        public double? CheckoutLatitude { get; set; }

        [BsonElement("checkoutLongitude")]
        public double? CheckoutLongitude { get; set; }

        [BsonElement("createdAt")]
        public string CreatedAt { get; set; }

        [BsonElement("plantCode")]
        public string PlantCode { get; set; }

        [BsonElement("updatedAt")]
        public string UpdatedAt { get; set; }

        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonElement("usedInDcu")]
        public bool UsedInDcu { get; set; }

        [BsonElement("usedInPretrip")]
        public bool UsedInPretrip { get; set; }

        [BsonElement("workHours")]
        public string WorkHours { get; set; }
    }
}

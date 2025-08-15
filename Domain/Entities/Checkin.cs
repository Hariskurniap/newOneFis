using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Checkin
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("amtEmployeeId")]
    public string AmtEmployeeId { get; set; }  // langsung string

    [BsonElement("amtEmployeeName")]
    public string AmtEmployeeName { get; set; }

    [BsonElement("checkinLatitude")]
    public double CheckinLatitude { get; set; }

    [BsonElement("checkinLongitude")]
    public double CheckinLongitude { get; set; }

    [BsonElement("checkinDate")]
    public string CheckinDate { get; set; }

    [BsonElement("checkoutDate")]
    public string CheckoutDate { get; set; }

    [BsonElement("checkoutLatitude")]
    public double? CheckoutLatitude { get; set; }

    [BsonElement("checkoutLongitude")]
    public double? CheckoutLongitude { get; set; }

    [BsonElement("workHours")]
    public string WorkHours { get; set; }

    [BsonElement("updatedBy")]
    public string UpdatedBy { get; set; }

    [BsonElement("usedInPretrip")]
    public bool UsedInPretrip { get; set; }

    [BsonElement("usedInDcu")]
    public bool UsedInDcu { get; set; }

    [BsonElement("plantCode")]
    public string PlantCode { get; set; }

    [BsonElement("createdAt")]
    public string CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public string UpdatedAt { get; set; }
}

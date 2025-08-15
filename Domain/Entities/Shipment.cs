using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Shipment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("additionalInfo")]
        public List<AdditionalInfoItem> AdditionalInfo { get; set; } = new();

        [BsonElement("bulkShipmentType")]
        public string? BulkShipmentType { get; set; }

        [BsonElement("carrier")]
        public string? Carrier { get; set; }

        [BsonElement("compartmentAllocation")]
        public List<CompartmentAllocationItem> CompartmentAllocation { get; set; } = new();

        [BsonElement("driverAllocation")]
        public List<DriverAllocationItem> DriverAllocation { get; set; } = new();

        [BsonElement("gateInBy")]
        public string? GateInBy { get; set; }

        [BsonElement("gateInTimestamp")]
        public string? GateInTimestamp { get; set; }

        [BsonElement("gateOutBy")]
        public string? GateOutBy { get; set; }

        [BsonElement("gateOutTimestamp")]
        public string? GateOutTimestamp { get; set; }

        [BsonElement("loadedTimestamp")]
        public string? LoadedTimestamp { get; set; }

        [BsonElement("onefisCreatedAt")]
        public string? OnefisCreatedAt { get; set; }

        [BsonElement("onefisCreatedBy")]
        public string? OnefisCreatedBy { get; set; }

        [BsonElement("onefisShipmentCode")]
        public string? OnefisShipmentCode { get; set; }

        [BsonElement("onefisShipmentStatus")]
        public string? OnefisShipmentStatus { get; set; }

        [BsonElement("onefisUpdatedAt")]
        public string? OnefisUpdatedAt { get; set; }

        [BsonElement("onefisUpdatedBy")]
        public string? OnefisUpdatedBy { get; set; }

        [BsonElement("plant")]
        public string? Plant { get; set; }

        [BsonElement("quantitySeal")]
        public object? QuantitySealRaw { get; set; }

        [BsonIgnore]
        public string? QuantitySeal => QuantitySealRaw?.ToString();

        [BsonElement("route")]
        public string? Route { get; set; }

        [BsonElement("sealStructure")]
        public List<SealStructureItem> SealStructure { get; set; } = new();

        [BsonElement("shipmentDate")]
        public string? ShipmentDate { get; set; }

        [BsonElement("shipmentEndTimestamp")]
        public string? ShipmentEndTimestamp { get; set; }

        [BsonElement("shipmentNumber")]
        public string? ShipmentNumber { get; set; }

        [BsonElement("shipmentStatus")]
        public string? ShipmentStatus { get; set; }

        [BsonElement("vehicleNumber")]
        public string? VehicleNumber { get; set; }

        [BsonElement("gpsTrackingUrl")]
        public string? GpsTrackingUrl { get; set; }

        [BsonElement("lastPositionLong")]
        public string? LastPositionLong { get; set; }

        [BsonElement("lastPositionLat")]
        public string? LastPositionLat { get; set; }

        [BsonElement("transactionPretripInspectionId")]
        public string? TransactionPretripInspectionId { get; set; }

        [BsonElement("pushSapShipment")]
        public bool PushSapShipment { get; set; }

        [BsonElement("pushSapGi")]
        public bool PushSapGi { get; set; }
    }

    public class AdditionalInfoItem
    {
        [BsonElement("dispathcer")] // Note: This matches the typo in your database
        public string? Dispatcher { get; set; }

        [BsonElement("ownUseRp")]
        public string? OwnUseRp { get; set; }

        [BsonElement("ownUseVolume")]
        public string? OwnUseVolume { get; set; }

        [BsonElement("ratio")]
        public string? Ratio { get; set; }

        [BsonElement("spbuownuse")]
        public string? Spbuownuse { get; set; }

        [BsonElement("tollRoadRp")]
        public string? TollRoadRp { get; set; }
    }

    public class CompartmentAllocationItem
    {
        [BsonElement("deliveryNumber")]
        public string? DeliveryNumber { get; set; }

        [BsonElement("distance")]
        public string? Distance { get; set; }

        [BsonElement("materialCode")]
        public string? MaterialCode { get; set; }

        [BsonElement("materialName")]
        public string? MaterialName { get; set; }

        [BsonElement("quantity")]
        public string? Quantity { get; set; }

        [BsonElement("shipToCode")]
        public string? ShipToCode { get; set; }

        [BsonElement("shipToName")]
        public string? ShipToName { get; set; }

        [BsonElement("soldToCode")]
        public string? SoldToCode { get; set; }

        [BsonElement("soldToName")]
        public string? SoldToName { get; set; }

        [BsonElement("spbuCode")]
        public string? SpbuCode { get; set; }

        [BsonElement("depotCode")]
        public string? DepotCode { get; set; }

        [BsonElement("depotDesc")]
        public string? DepotDesc { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }

        [BsonElement("density")]
        public string? Density { get; set; }

        [BsonElement("temperature")]
        public string? Temperature { get; set; }

        [BsonElement("oilLevel")]
        public string? OilLevel { get; set; }

        [BsonElement("isPrint")]
        public bool IsPrint { get; set; }

        [BsonElement("countPrint")]
        public int CountPrint { get; set; }

        [BsonElement("sealNumber")]
        public string? SealNumber { get; set; }

        [BsonElement("compartment")]
        public string? Compartment { get; set; }
    }

    public class DriverAllocationItem
    {
        [BsonElement("driverName")]
        public string? DriverName { get; set; }

        [BsonElement("driverCode")]
        public string? DriverCode { get; set; }

        [BsonElement("driverRole")]
        public string? DriverRole { get; set; }
    }

    public class SealStructureItem
    {
        [BsonElement("sealNumber")]
        public string? SealNumber { get; set; }
    }
}
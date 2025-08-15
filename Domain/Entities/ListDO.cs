using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class ListDO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("actualGiDate")]
        public string? ActualGiDate { get; set; }

        [BsonElement("actualGiTIme")] // Perhatikan huruf besar 'TIme'
        public string? ActualGiTime { get; set; }

        [BsonElement("conditionDelivery")]
        public string? ConditionDelivery { get; set; }

        [BsonElement("conditionShipfrom")]
        public string? ConditionShipfrom { get; set; }

        [BsonElement("conditionShipping")]
        public string? ConditionShipping { get; set; }

        [BsonElement("customerPoDate")]
        public string? CustomerPoDate { get; set; }

        [BsonElement("customerPoNumber")]
        public string? CustomerPoNumber { get; set; }

        [BsonElement("deliveryDate")]
        public string? DeliveryDate { get; set; }

        [BsonElement("deliveryNumber")]
        public string? DeliveryNumber { get; set; }

        [BsonElement("deliveryQty")]
        public string? DeliveryQty { get; set; }

        [BsonElement("distributionChannel")]
        public string? DistributionChannel { get; set; }

        [BsonElement("materialCode")]
        public string? MaterialCode { get; set; }

        [BsonElement("materialName")]
        public string? MaterialName { get; set; }

        [BsonElement("driverNumber")]
        public List<DriverInfo>? DriverNumber { get; set; }

        [BsonElement("giStatus")]
        public string? GiStatus { get; set; }

        [BsonElement("giStatus_Desc")]
        public string? GiStatusDesc { get; set; }

        [BsonElement("items")]
        public List<Item>? Items { get; set; }

        [BsonElement("netweight")]
        public string? Netweight { get; set; }

        [BsonElement("onefisCreatedAt")]
        public object? OnefisCreatedAt { get; set; }

        [BsonIgnore]
        public string? OnefisCreatedAtStr => OnefisCreatedAt?.ToString();

        [BsonElement("onefisCreatedBy")]
        public string? OnefisCreatedBy { get; set; }

        [BsonElement("onefisDoStatus")]
        public string? OnefisDoStatus { get; set; }

        [BsonElement("onefisDoStatusDesc")]
        public string? OnefisDoStatusDesc { get; set; }

        [BsonElement("onefisEvidenceDeletedDos")]
        public List<string>? OnefisEvidenceDeletedDos { get; set; }

        [BsonElement("onefisGetDetailCreatedBy")]
        public string? OnefisGetDetailCreatedBy { get; set; }

        [BsonElement("onefisGetDetailUpdatedBy")]
        public string? OnefisGetDetailUpdatedBy { get; set; }

        [BsonElement("onefisRemarkDeletedDo")]
        public string? OnefisRemarkDeletedDo { get; set; }

        [BsonElement("onefisUpdatedAt")]
        public object? OnefisUpdatedAt { get; set; }

        [BsonIgnore]
        public string? OnefisUpdatedAtStr => OnefisUpdatedAt?.ToString();

        [BsonElement("onefisUpdatedBy")]
        public string? OnefisUpdatedBy { get; set; }

        [BsonElement("onfisGetDetailCreatedAt")] // Perhatikan 'onfis' bukan 'onefis'
        public string? OnfisGetDetailCreatedAt { get; set; }

        [BsonElement("onfisGetDetailUpdatedAt")]
        public string? OnfisGetDetailUpdatedAt { get; set; }

        [BsonElement("plannedGiDate")]
        public string? PlannedGiDate { get; set; }

        [BsonElement("plant")]
        public string? Plant { get; set; }

        [BsonElement("salesOrder")]
        public string? SalesOrder { get; set; }

        [BsonElement("salesOrderDate")]
        public string? SalesOrderDate { get; set; }

        [BsonElement("salesOrg")]
        public string? SalesOrg { get; set; }

        [BsonElement("shipmentNumber")]
        public string? ShipmentNumber { get; set; }

        [BsonElement("shippingPoint")]
        public string? ShippingPoint { get; set; }

        [BsonElement("shipToAddress")]
        public string? ShipToAddress { get; set; }

        [BsonElement("shipToCity")]
        public string? ShipToCity { get; set; }

        [BsonElement("shipToCode")]
        public string? ShipToCode { get; set; }

        [BsonElement("shipToName")]
        public string? ShipToName { get; set; }

        [BsonElement("shipToPostalCode")]
        public string? ShipToPostalCode { get; set; }

        [BsonElement("totalVolume")]
        public string? TotalVolume { get; set; }

        [BsonElement("totalWeight")]
        public string? TotalWeight { get; set; }

        [BsonElement("transporter")]
        public string? Transporter { get; set; }

        [BsonElement("vehicleNumber")]
        public string? VehicleNumber { get; set; }

        [BsonElement("volumeUOM")] // Perhatikan huruf besar 'UOM'
        public string? VolumeUom { get; set; }

        [BsonElement("weightUom")]
        public string? WeightUom { get; set; }

        [BsonElement("additionalProp1")]
        public List<string>? AdditionalProp1 { get; set; }

        [BsonElement("soldToCode")]
        public string? SoldToCode { get; set; }

        [BsonElement("soldToDescription")]
        public string? SoldToDescription { get; set; }

        [BsonElement("depotCode")]
        public string? DepotCode { get; set; }

        [BsonElement("depotDesc")]
        public string? DepotDesc { get; set; }

        [BsonElement("customerGroup")]
        public string? CustomerGroup { get; set; }

        [BsonElement("spbuCode")]
        public string? SpbuCode { get; set; }
    }

    public class DriverInfo
    {
        [BsonElement("driverId")]
        public string? DriverId { get; set; }

        [BsonElement("driverName")]
        public string? DriverName { get; set; }
    }

    public class Item
    {
        [BsonElement("density")]
        public string? Density { get; set; }

        [BsonElement("densityUom")]
        public string? DensityUom { get; set; }

        [BsonElement("itemNumber")]
        public string? ItemNumber { get; set; }

        [BsonElement("material")]
        public string? Material { get; set; }

        [BsonElement("materialName")]
        public string? MaterialName { get; set; }

        [BsonElement("qty")]
        public string? Qty { get; set; }

        [BsonElement("qtyUom")]
        public string? QtyUom { get; set; }

        [BsonElement("temp")]
        public string? Temp { get; set; }

        [BsonElement("tempUom")]
        public string? TempUom { get; set; }

        [BsonElement("weight")]
        public string? Weight { get; set; }

        [BsonElement("weightUom")]
        public string? WeightUom { get; set; }
    }
}
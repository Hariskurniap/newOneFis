using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace YourNamespace.Entities
{
    public class TransactionPretripInspections
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("vehicleNumber")]
        public string VehicleNumber { get; set; }

        [BsonElement("dailyInspectionDate")]
        public string DailyInspectionDate { get; set; }

        [BsonElement("dailyInspectionId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DailyInspectionId { get; set; }

        [BsonElement("pretripInspectionPlanCode")]
        public string PretripInspectionPlanCode { get; set; }

        [BsonElement("pretripInspectionInitiateDate")]
        public string PretripInspectionInitiateDate { get; set; }

        [BsonElement("pretripInspectionInitiateBy")]
        public string PretripInspectionInitiateBy { get; set; }

        [BsonElement("pretripInspectionInitiateCode")]
        public string PretripInspectionInitiateCode { get; set; }

        [BsonElement("pretripInspectionInitiateResult")]
        public string PretripInspectionInitiateResult { get; set; }

        [BsonElement("pretripInspectionInitiateNote")]
        public string PretripInspectionInitiateNote { get; set; }

        [BsonElement("pretripInspectionInitiateEvidence")]
        public string PretripInspectionInitiateEvidence { get; set; }

        [BsonElement("pretripInspectionReviewDate")]
        public DateTime? PretripInspectionReviewDate { get; set; }

        [BsonElement("pretripInspectionReviewBy")]
        public string PretripInspectionReviewBy { get; set; }

        [BsonElement("pretripInspectionReviewCode")]
        public string PretripInspectionReviewCode { get; set; }

        [BsonElement("pretripInspectionReviewResult")]
        public string PretripInspectionReviewResult { get; set; }

        [BsonElement("pretripInspectionReviewNote")]
        public string PretripInspectionReviewNote { get; set; }

        [BsonElement("pretripInspectionReviewEvidence")]
        public string PretripInspectionReviewEvidence { get; set; }

        [BsonElement("pretripInspectionApproveDate")]
        public DateTime? PretripInspectionApproveDate { get; set; }

        [BsonElement("pretripInspectionApproveBy")]
        public string PretripInspectionApproveBy { get; set; }

        [BsonElement("pretripInspectionApproveCode")]
        public string PretripInspectionApproveCode { get; set; }

        [BsonElement("pretripInspectionApproveResult")]
        public string PretripInspectionApproveResult { get; set; }

        [BsonElement("pretripInspectionApproveNote")]
        public string PretripInspectionApproveNote { get; set; }

        [BsonElement("pretripInspectionApproveEvidence")]
        public string PretripInspectionApproveEvidence { get; set; }

        [BsonElement("driverCollections")]
        public List<DriverCollection> DriverCollections { get; set; }

        [BsonElement("transactionPretripInspections")]
        public List<TransactionPretripInspectionGroup> TransactionPretripInspectionsGroups { get; set; }

        [BsonElement("isUsedInShipment")]
        public bool IsUsedInShipment { get; set; }
    }

    public class DriverCollection
    {
        [BsonElement("driverName")]
        public string DriverName { get; set; }

        [BsonElement("driverCode")]
        public string DriverCode { get; set; }

        [BsonElement("driverRole")]
        public string DriverRole { get; set; }
    }

    public class TransactionPretripInspectionGroup
    {
        [BsonElement("group")]
        public string Group { get; set; }

        [BsonElement("details")]
        public List<TransactionPretripInspectionDetail> Details { get; set; }
    }

    public class TransactionPretripInspectionDetail
    {
        [BsonElement("component")]
        public string Component { get; set; }

        [BsonElement("criteria")]
        public string Criteria { get; set; }

        [BsonElement("purpose")]
        public string Purpose { get; set; }

        [BsonElement("result")]
        public string Result { get; set; }

        [BsonElement("notes")]
        public string Notes { get; set; }

        [BsonElement("evidence")]
        public string Evidence { get; set; }
    }
}

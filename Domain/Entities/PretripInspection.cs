using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class PretripInspection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("componentInspections")]
        public List<PretripComponentInspection> ComponentInspections { get; set; }
    }

    public class PretripComponentInspection
    {
        [BsonElement("group")]
        public string Group { get; set; }

        [BsonElement("details")]
        public List<PretripInspectionDetail> Details { get; set; }
    }

    public class PretripInspectionDetail
    {
        [BsonElement("komponen")]
        public string Komponen { get; set; }

        [BsonElement("tujuan")]
        public string Tujuan { get; set; }

        [BsonElement("penilaian")]
        public string Penilaian { get; set; }
    }
}

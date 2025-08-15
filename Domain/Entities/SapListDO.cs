namespace Domain.Entities
{
    public class SapListDOResponse
    {
        public MtGetListDOResponse mt_getListDOResponse { get; set; } = null!;
    }

    public class MtGetListDOResponse
    {
        public List<Detail> Details { get; set; } = new();
        public List<Message>? Message { get; set; }
    }

    public class Detail
    {
        public string? Depot { get; set; }
        public string? Ship_to { get; set; }
        public string? Ship_name { get; set; }
        public string? Delivery_Number { get; set; }
        public string? Shipment_Number { get; set; }
        public string? Transporter { get; set; }
        public string? Vehicle_Number { get; set; }
        public string? Driver_Number { get; set; }
        public string? Delivery_Qty { get; set; }
        public string? UOM { get; set; }
        public string? Planned_GI_Date { get; set; }
        public string? GI_Status { get; set; }
        public string? GI_Status_Desc { get; set; }
        public string? Actual_GI_Date { get; set; }
        public string? Actual_GI_Time { get; set; }
        public string? Shipping_point { get; set; }
        public string? Material_code { get; set; }
        public string? Material_name { get; set; }
        public string? Sales_org { get; set; }
        public string? Distribution_Channel { get; set; }
    }

    public class Message
    {
        public string? Type { get; set; }
        public string? Code { get; set; }
        public string? Desc_Msg { get; set; }
    }
}

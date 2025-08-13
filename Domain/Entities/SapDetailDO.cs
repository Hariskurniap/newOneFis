namespace Domain.Entities
{
    public class SapDetailDO
    {
        public SapDetailDOResponse mt_getDetailDOResponse { get; set; } = null!;
    }

    public class SapDetailDOResponse
    {
        public SapDeliveryDetails DeliveryDetails { get; set; } = null!;
        public List<SapMessage>? Message { get; set; }
    }

    public class SapDeliveryDetails
    {
        public string? Shipment_Number { get; set; }
        public string? Delivery_Number { get; set; }
        public string? Delivery_Date { get; set; }
        public string? Ship_to_Code { get; set; }
        public string? Ship_to_name { get; set; }
        public string? Ship_to_address { get; set; }
        public string? Ship_to_City { get; set; }
        public string? Ship_to_postalcode { get; set; }
        public string? Customer_PO_Number { get; set; }
        public string? Customer_PO_Date { get; set; }
        public string? Order_Number { get; set; }
        public string? Order_Date { get; set; }
        public string? Condition_Shipping { get; set; }
        public string? Condition_Delivery { get; set; }
        public string? Condition_Ship_from { get; set; }
        public string? Total_Weight { get; set; }
        public string? Net_weight { get; set; }
        public string? Weight_UOM { get; set; }
        public string? Total_Volume { get; set; }
        public string? Volume_UOM { get; set; }
        public string? GI_Status { get; set; }
        public string? GI_Status_Desc { get; set; }
        public string? Plant { get; set; }
        public string? Actual_GI_Date { get; set; }
        public string? Actual_GI_TIme { get; set; }
        public List<SapItem>? Items { get; set; }
    }

    public class SapItem
    {
        public string? Item_number { get; set; }
        public string? Material { get; set; }
        public string? Material_Name { get; set; }
        public string? Qty { get; set; }
        public string? QTY_UOM { get; set; }
        public string? Weight { get; set; }
        public string? Weight_UOM { get; set; }
    }

    public class SapMessage
    {
        public string? Type { get; set; }
        public string? Code { get; set; }
        public string? Desc_Msg { get; set; }
    }
}

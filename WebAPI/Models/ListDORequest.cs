namespace WebAPI.Models;

public class ListDORequest
{
    public string Depot { get; set; } = "";
    public string Shipping_point { get; set; } = "";
    public string Delivery_date_From { get; set; } = "";
    public string Delivery_date_To { get; set; } = "";
    public string Ship_to { get; set; } = "";
    public string Delivery_time_From { get; set; } = "";
    public string Delivery_time_to { get; set; } = "";
    public string DO_Type { get; set; } = "";
    public string Delivery_Number { get; set; } = "";
    public string GI_Status { get; set; } = "";
}

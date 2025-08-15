namespace WebAPI.Models
{
    public class MinimalListDORequest
    {
        public string Shipping_point { get; set; } = null!;
        public string Delivery_Number { get; set; } = null!;
        public string GI_Status { get; set; } = null!;
    }

}

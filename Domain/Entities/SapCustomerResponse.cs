namespace Domain.Entities
{
    public class SapCustomerResponse
    {
        public MtGetCustomerResponse mt_getCustomerResponse { get; set; } = null!;
    }

    public class MtGetCustomerResponse
    {
        public List<CustomerDetail>? Details { get; set; }
        public List<Message>? Message { get; set; }
    }

    public class CustomerDetail
    {
        public string? Sold_to_code { get; set; }
        public string? Sold_to_description { get; set; }
        public string? Ship_to_Code { get; set; }
        public string? Ship_to_Description { get; set; }
        public string? Ship_to_Address { get; set; }
        public string? Depot { get; set; }
        public string? Desc_Depot { get; set; }
        public string? Customer_Group { get; set; }
        public string? Name2 { get; set; }
    }
}

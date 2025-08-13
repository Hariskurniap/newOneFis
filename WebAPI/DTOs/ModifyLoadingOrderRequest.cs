using Domain.Entities;

namespace WebAPI.DTOs
{
    public class ModifyLoadingOrderRequest
    {
        public string? DeliveryNumber { get; set; }
        public string? Plant { get; set; }

        public string? OnefisCreatedAt { get; set; }
        public string? OnefisCreatedBy { get; set; }

        public string? OnefisUpdatedAt { get; set; }
        public string? OnefisUpdatedBy { get; set; }

        public ListDO Datas { get; set; } = new ListDO();
    }
}

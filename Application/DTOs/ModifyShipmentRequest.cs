using Domain.Entities;

namespace Application.DTOs
{
    public class ModifyShipmentRequest
    {
        public string? OnefisShipmentCode { get; set; }

        public string? OnefisCreatedAt { get; set; }
        public string? OnefisCreatedBy { get; set; }

        public string? OnefisUpdatedAt { get; set; }
        public string? OnefisUpdatedBy { get; set; }

        public Shipment Datas { get; set; } = new Shipment();
    }
}

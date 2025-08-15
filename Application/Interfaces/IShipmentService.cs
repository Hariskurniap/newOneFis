using Application.DTOs;
using Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IShipmentService
    {
        Task<List<Shipment>> GetShipmentsAsync();
        Task<Shipment> GetShipmentByIdAsync(string id);
        Task<Shipment> GetShipmentByCodeAsync(string onefisShipmentCode); // NEW
        Task<bool> UpdateShipmentAsync(ModifyShipmentRequest request);
    }
}

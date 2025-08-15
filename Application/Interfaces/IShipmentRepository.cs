using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IShipmentRepository
    {
        Task<List<Shipment>> GetAllAsync();
        Task<Shipment> GetByIdAsync(string id);
        Task InsertAsync(Shipment shipment);
        Task<bool> UpdateAsync(string onefisShipmentCode, Shipment shipment);
        Task<Shipment> GetByShipmentCodeAsync(string onefisShipmentCode);
    }
}

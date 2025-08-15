using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ShipmentService : IShipmentService
{
    private readonly IMongoCollection<Shipment> _shipments;

    public ShipmentService(IMongoDbContext context)
    {
        _shipments = context.Database.GetCollection<Shipment>("Shipments");
    }

    public async Task<List<Shipment>> GetShipmentsAsync() =>
        await _shipments.Find(_ => true).ToListAsync();

    public async Task<Shipment> GetShipmentByIdAsync(string id) =>
        await _shipments.Find(s => s.Id == id).FirstOrDefaultAsync();

    public async Task<Shipment> GetShipmentByCodeAsync(string onefisShipmentCode) =>
        await _shipments.Find(s => s.OnefisShipmentCode == onefisShipmentCode).FirstOrDefaultAsync();

    public async Task<bool> UpdateShipmentAsync(ModifyShipmentRequest request)
    {
        if (string.IsNullOrEmpty(request.OnefisShipmentCode))
            throw new ArgumentException("OnefisShipmentCode wajib diisi untuk update.");

        var updateDef = Builders<Shipment>.Update
            .Set(x => x.OnefisUpdatedAt, request.OnefisUpdatedAt)
            .Set(x => x.OnefisUpdatedBy, request.OnefisUpdatedBy);

        if (!string.IsNullOrEmpty(request.Datas.ShipmentStatus))
            updateDef = updateDef.Set(x => x.ShipmentStatus, request.Datas.ShipmentStatus);

        if (!string.IsNullOrEmpty(request.Datas.VehicleNumber))
            updateDef = updateDef.Set(x => x.VehicleNumber, request.Datas.VehicleNumber);

        var result = await _shipments.UpdateOneAsync(
            s => s.OnefisShipmentCode == request.OnefisShipmentCode,
            updateDef
        );

        return result.ModifiedCount > 0;
    }
}

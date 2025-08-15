using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly IMongoCollection<Shipment> _collection;

        public ShipmentRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<Shipment>("Shipments");
        }

        public async Task<List<Shipment>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Shipment> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Shipment shipment)
        {
            await _collection.InsertOneAsync(shipment);
        }

        public async Task<bool> UpdateAsync(string onefisShipmentCode, Shipment shipment)
        {
            var filter = Builders<Shipment>.Filter.Eq(x => x.OnefisShipmentCode, onefisShipmentCode);

            // Update hanya field yang tidak null (partial update)
            var update = Builders<Shipment>.Update
                .Set(x => x.OnefisUpdatedAt, shipment.OnefisUpdatedAt)
                .Set(x => x.OnefisUpdatedBy, shipment.OnefisUpdatedBy);

            // Bisa ditambahkan field lain jika tidak null
            if (!string.IsNullOrEmpty(shipment.VehicleNumber))
                update = update.Set(x => x.VehicleNumber, shipment.VehicleNumber);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<Shipment> GetByShipmentCodeAsync(string onefisShipmentCode)
        {
            return await _collection.Find(x => x.OnefisShipmentCode == onefisShipmentCode).FirstOrDefaultAsync();
        }

    }
}

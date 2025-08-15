using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DailyInspectionRepository : IDailyInspectionRepository
    {
        private readonly IMongoCollection<DailyInspection> _collection;

        public DailyInspectionRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<DailyInspection>("DailyInspection");
        }

        public async Task<List<DailyInspection>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<DailyInspection> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertAsync(DailyInspection inspection)
        {
            await _collection.InsertOneAsync(inspection);
            return true;
        }

        public async Task<bool> UpdateAsync(string id, DailyInspection inspection)
        {
            var result = await _collection.ReplaceOneAsync(x => x.Id == id, inspection);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }
    }
}

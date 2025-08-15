using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DcuCheckinRepository : IDcuCheckinRepository
    {
        private readonly IMongoCollection<DcuCheckin> _collection;

        public DcuCheckinRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<DcuCheckin>("DailyCheckups");
        }

        public async Task<List<DcuCheckin>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(string id, DcuCheckin data)
        {
            var filter = Builders<DcuCheckin>.Filter.Eq(d => d.Id, id);
            var update = Builders<DcuCheckin>.Update
                .Set(d => d.DcuEmployeeId, data.DcuEmployeeId)
                .Set(d => d.DcuName, data.DcuName)
                .Set(d => d.AmtEmployeeId, data.AmtEmployeeId)
                .Set(d => d.AmtEmployeeName, data.AmtEmployeeName)
                .Set(d => d.Status, data.Status)
                .Set(d => d.Result, data.Result)
                .Set(d => d.BodyTemperature, data.BodyTemperature)
                .Set(d => d.BloodPressure, data.BloodPressure)
                .Set(d => d.OxygenLevel, data.OxygenLevel)
                .Set(d => d.Information, data.Information)
                .Set(d => d.BloodSugar, data.BloodSugar)
                .Set(d => d.DcuDate, data.DcuDate)
                .Set(d => d.UpdatedAt, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Set(d => d.UpdatedBy, data.UpdatedBy)
                .Set(d => d.PlantCode, data.PlantCode);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> InsertAsync(DcuCheckin data)
        {
            await _collection.InsertOneAsync(data);
            return true;
        }

        public async Task<DcuCheckin> GetByIdAsync(string id)
        {
            var filter = Builders<DcuCheckin>.Filter.Eq(d => d.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

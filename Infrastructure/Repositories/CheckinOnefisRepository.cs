using Domain.Entities;
using Application.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CheckinOnefisRepository : ICheckinOnefisRepository
    {
        private readonly IMongoCollection<CheckinOnefis> _collection;

        public CheckinOnefisRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<CheckinOnefis>("CheckinsOnefis");
        }

        public async Task<CheckinOnefis> GetByAttendanceCodeAsync(string attendanceCode)
        {
            return await _collection.Find(c => c.AttendaceCode == attendanceCode).FirstOrDefaultAsync();
        }

        public async Task<List<CheckinOnefis>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task UpsertAsync(CheckinOnefis checkinOnefis)
        {
            var filter = Builders<CheckinOnefis>.Filter.Eq(c => c.AttendaceCode, checkinOnefis.AttendaceCode);
            var options = new ReplaceOptions { IsUpsert = true };
            await _collection.ReplaceOneAsync(filter, checkinOnefis, options);
        }
    }
}

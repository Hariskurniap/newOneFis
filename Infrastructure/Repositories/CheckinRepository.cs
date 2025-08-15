using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Mongo;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CheckinRepository : ICheckinRepository
    {
        private readonly IMongoCollection<Checkin> _collection;

        public CheckinRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<Checkin>("Checkins");
        }

        public async Task<List<Checkin>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Checkin> GetByAmtEmployeeIdAndEmptyCheckoutAsync(string amtEmployeeId)
        {
            return await _collection.Find(c => c.AmtEmployeeId == amtEmployeeId && string.IsNullOrEmpty(c.CheckoutDate))
                                    .FirstOrDefaultAsync();
        }

        public async Task UpsertAsync(Checkin checkin)
        {
            if (string.IsNullOrEmpty(checkin.Id))
            {
                await _collection.InsertOneAsync(checkin);
            }
            else
            {
                await _collection.ReplaceOneAsync(c => c.Id == checkin.Id, checkin, new ReplaceOptions { IsUpsert = true });
            }
        }
    }
}

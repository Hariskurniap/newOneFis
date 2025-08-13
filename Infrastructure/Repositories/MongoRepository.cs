using Domain.Entities;
using MongoDB.Driver;
using Application.Interfaces;
using MongoDB.Bson;

namespace Infrastructure.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoCollection<ListDO> _collection;

        public MongoRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<ListDO>("ListDO");
        }

        public async Task<bool> CheckDOExistAsync(string deliveryNumber, string plant)
        {
            var filter = Builders<ListDO>.Filter.And(
                Builders<ListDO>.Filter.Eq(x => x.DeliveryNumber, deliveryNumber),
                Builders<ListDO>.Filter.Eq(x => x.Plant, plant)
            );

            return await _collection.Find(filter).AnyAsync();
        }

        public async Task InsertListDOAsync(ListDO listDO)
        {
            await _collection.InsertOneAsync(listDO);
        }

        public async Task InsertManyAsync(IEnumerable<ListDO> listDOs)
        {
            if (listDOs != null && listDOs.Any())
            {
                await _collection.InsertManyAsync(listDOs);
            }
        }

        public async Task<bool> ModifyListDOAsync(string deliveryNumber, string plant, ListDO updatedDO)
        {
            var filter = Builders<ListDO>.Filter.And(
                Builders<ListDO>.Filter.Eq(x => x.DeliveryNumber, deliveryNumber),
                Builders<ListDO>.Filter.Eq(x => x.Plant, plant)
            );

            var result = await _collection.ReplaceOneAsync(filter, updatedDO);
            return result.MatchedCount > 0;
        }

    }
}

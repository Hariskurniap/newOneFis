using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class ShipmentChecklistActivitiesRepository : IShipmentChecklistActivitiesRepository
    {
        private readonly IMongoCollection<ShipmentChecklistActivities> _collection;

        public ShipmentChecklistActivitiesRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<ShipmentChecklistActivities>("ShipmentChecklistActivities");
        }

        public IQueryable<ShipmentChecklistActivities> GetAll()
        {
            return _collection.AsQueryable();
        }

        public ShipmentChecklistActivities GetById(string id)
        {
            return _collection.AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public void Create(ShipmentChecklistActivities entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(string id, ShipmentChecklistActivities entity)
        {
            _collection.ReplaceOne(x => x.Id == id, entity);
        }
    }
}

using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class MasterDailyInspectionRepository : IMasterDailyInspectionRepository
    {
        private readonly IMongoCollection<MasterDailyInspection> _collection;

        public MasterDailyInspectionRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<MasterDailyInspection>("MasterDailyInspection");
        }

        public IQueryable<MasterDailyInspection> GetAll()
        {
            return _collection.AsQueryable();
        }

        public MasterDailyInspection GetById(string id)
        {
            return _collection.AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public void Create(MasterDailyInspection entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(string id, MasterDailyInspection entity)
        {
            _collection.ReplaceOne(x => x.Id == id, entity);
        }
    }
}

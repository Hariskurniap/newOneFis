using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class ShipmentValidationsRepository : IShipmentValidationsRepository
    {
        private readonly IMongoCollection<ShipmentValidations> _collection;

        public ShipmentValidationsRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<ShipmentValidations>("ShipmentValidations");
        }

        public IQueryable<ShipmentValidations> GetAll()
        {
            return _collection.AsQueryable();
        }

        public ShipmentValidations GetById(string id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Create(ShipmentValidations entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(string id, ShipmentValidations entity)
        {
            _collection.ReplaceOne(x => x.Id == id, entity);
        }
    }
}

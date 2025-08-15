using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using System.Linq;
using YourNamespace.Entities;

namespace Infrastructure.Repositories
{
    public class TransactionPretripInspectionsRepository : ITransactionPretripInspectionsRepository
    {
        private readonly IMongoCollection<TransactionPretripInspections> _collection;

        public TransactionPretripInspectionsRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<TransactionPretripInspections>("TransactionPretripInspections");
        }

        public IQueryable<TransactionPretripInspections> GetAll() => _collection.AsQueryable();

        public TransactionPretripInspections GetById(string id) => _collection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(TransactionPretripInspections entity) => _collection.InsertOne(entity);

        public void Update(string id, TransactionPretripInspections entity) => _collection.ReplaceOne(x => x.Id == id, entity);
    }
}

using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class PretripInspectionRepository : IPretripInspectionRepository
    {
        private readonly IMongoCollection<PretripInspection> _collection;

        public PretripInspectionRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<PretripInspection>("PretripInspection");
        }

        public IQueryable<PretripInspection> GetAll()
        {
            return _collection.AsQueryable();
        }
    }
}

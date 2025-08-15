using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Application.Services
{
    public class ListDOService : IListDOService
    {
        private readonly IMongoDbContext _context;

        public ListDOService(IMongoDbContext context)
        {
            _context = context;
        }

        public IQueryable<ListDO> GetListDO()
        {
            return _context.ListDOCollection.AsQueryable();
        }
    }
}

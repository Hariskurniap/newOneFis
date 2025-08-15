using Domain.Entities;
using System.Linq;
using YourNamespace.Entities;

namespace Application.Interfaces
{
    public interface ITransactionPretripInspectionsRepository
    {
        IQueryable<TransactionPretripInspections> GetAll();
        TransactionPretripInspections GetById(string id);
        void Create(TransactionPretripInspections entity);
        void Update(string id, TransactionPretripInspections entity);
    }
}

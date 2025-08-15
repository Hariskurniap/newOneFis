using Domain.Entities;
using System.Collections.Generic;
using YourNamespace.Entities;

namespace Application.Interfaces
{
    public interface ITransactionPretripInspectionsService
    {
        IEnumerable<TransactionPretripInspections> GetAll();
        TransactionPretripInspections GetById(string id);
        void Create(TransactionPretripInspections entity);
        void Update(string id, TransactionPretripInspections entity);
    }
}

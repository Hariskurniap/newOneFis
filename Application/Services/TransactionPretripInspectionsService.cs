using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using YourNamespace.Entities;

namespace Application.Services
{
    public class TransactionPretripInspectionsService : ITransactionPretripInspectionsService
    {
        private readonly ITransactionPretripInspectionsRepository _repository;

        public TransactionPretripInspectionsService(ITransactionPretripInspectionsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TransactionPretripInspections> GetAll() => _repository.GetAll();

        public TransactionPretripInspections GetById(string id) => _repository.GetById(id);

        public void Create(TransactionPretripInspections entity) => _repository.Create(entity);

        public void Update(string id, TransactionPretripInspections entity) => _repository.Update(id, entity);
    }
}

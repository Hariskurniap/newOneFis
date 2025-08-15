using Application.Interfaces;
using Domain.Entities;
using System.Linq;

namespace Application.Services
{
    public class PretripInspectionService : IPretripInspectionService
    {
        private readonly IPretripInspectionRepository _repository;

        public PretripInspectionService(IPretripInspectionRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<PretripInspection> GetAll()
        {
            return _repository.GetAll();
        }
    }
}

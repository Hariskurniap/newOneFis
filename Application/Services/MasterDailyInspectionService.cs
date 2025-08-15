using Application.Interfaces;
using Domain.Entities;
using System.Linq;

namespace Application.Services
{
    public class MasterDailyInspectionService : IMasterDailyInspectionService
    {
        private readonly IMasterDailyInspectionRepository _repository;

        public MasterDailyInspectionService(IMasterDailyInspectionRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<MasterDailyInspection> GetAll()
        {
            return _repository.GetAll();
        }

        public MasterDailyInspection GetById(string id)
        {
            return _repository.GetById(id);
        }

        public void Create(MasterDailyInspection entity)
        {
            _repository.Create(entity);
        }

        public void Update(string id, MasterDailyInspection entity)
        {
            _repository.Update(id, entity);
        }
    }
}

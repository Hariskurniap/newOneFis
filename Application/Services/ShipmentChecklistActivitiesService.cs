using Application.Interfaces;
using Domain.Entities;
using System.Linq;

namespace Application.Services
{
    public class ShipmentChecklistActivitiesService : IShipmentChecklistActivitiesService
    {
        private readonly IShipmentChecklistActivitiesRepository _repository;

        public ShipmentChecklistActivitiesService(IShipmentChecklistActivitiesRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<ShipmentChecklistActivities> GetAll()
        {
            return _repository.GetAll();
        }

        public ShipmentChecklistActivities GetById(string id)
        {
            return _repository.GetById(id);
        }

        public void Create(ShipmentChecklistActivities entity)
        {
            _repository.Create(entity);
        }

        public void Update(string id, ShipmentChecklistActivities entity)
        {
            _repository.Update(id, entity);
        }
    }
}

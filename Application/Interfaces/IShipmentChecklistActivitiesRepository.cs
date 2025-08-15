using Domain.Entities;
using System.Linq;

namespace Application.Interfaces
{
    public interface IShipmentChecklistActivitiesRepository
    {
        IQueryable<ShipmentChecklistActivities> GetAll();
        ShipmentChecklistActivities GetById(string id);
        void Create(ShipmentChecklistActivities entity);
        void Update(string id, ShipmentChecklistActivities entity);
    }
}

using Domain.Entities;
using System.Linq;

namespace Application.Interfaces
{
    public interface IMasterDailyInspectionService
    {
        IQueryable<MasterDailyInspection> GetAll();
        MasterDailyInspection GetById(string id);
        void Create(MasterDailyInspection entity);
        void Update(string id, MasterDailyInspection entity);
    }
}

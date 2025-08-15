using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPretripInspectionRepository
    {
        IQueryable<PretripInspection> GetAll(); // Untuk OData
    }
}

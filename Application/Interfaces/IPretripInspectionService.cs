using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPretripInspectionService
    {
        IQueryable<PretripInspection> GetAll();
    }
}

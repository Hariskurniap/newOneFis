using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDcuCheckinService
    {
        Task<bool> ModifyAsync(string id, DcuCheckin data);
        Task<bool> InsertAsync(DcuCheckin data);
        Task<DcuCheckin> GetByIdAsync(string id);
        Task<List<DcuCheckin>> GetAllAsync();
    }
}

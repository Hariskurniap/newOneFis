using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDcuCheckinRepository
    {
        Task<bool> UpdateAsync(string id, DcuCheckin data);
        Task<bool> InsertAsync(DcuCheckin data);
        Task<DcuCheckin> GetByIdAsync(string id);
        Task<List<DcuCheckin>> GetAllAsync();
    }
}

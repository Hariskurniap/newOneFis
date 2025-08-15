using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDailyInspectionService
    {
        Task<List<DailyInspection>> GetAllAsync();
        Task<DailyInspection> GetByIdAsync(string id);
        Task<bool> CreateAsync(DailyInspection inspection);
        Task<bool> UpdateAsync(string id, DailyInspection inspection);
        Task<bool> DeleteAsync(string id);
    }
}

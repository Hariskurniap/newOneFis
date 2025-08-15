using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICheckinService
    {
        //IQueryable<Checkin> GetAllQueryable();
        Task<List<Checkin>> GetAllAsync();
        Task<Checkin> GetByAmtEmployeeIdAndEmptyCheckoutAsync(string amtEmployeeId);
        Task UpsertAsync(Checkin checkin);
        Task<(bool Success, string Message)> ModifyInsertAsync(IEnumerable<Checkin> datas);
        Task<(bool Success, string Message)> ModifyUpdateAsync(IEnumerable<Checkin> datas);
    }
}

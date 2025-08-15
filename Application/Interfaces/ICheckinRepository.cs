using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICheckinRepository
    {
        //IQueryable<Checkin> GetAllQueryable();
        Task<List<Checkin>> GetAllAsync();
        Task<Checkin> GetByAmtEmployeeIdAndEmptyCheckoutAsync(string amtEmployeeId);
        Task UpsertAsync(Checkin checkin);
    }
}

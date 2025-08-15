using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICheckinOnefisService
    {
        Task<List<CheckinOnefis>> GetAllAsync();
        Task<CheckinOnefis> GetByAttendanceCodeAsync(string code);
        Task UpsertAsync(CheckinOnefis checkinOnefis);
    }
}

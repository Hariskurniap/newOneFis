using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICheckinOnefisRepository
    {
        Task<CheckinOnefis> GetByAttendanceCodeAsync(string attendanceCode);
        Task<List<CheckinOnefis>> GetAllAsync();
        Task UpsertAsync(CheckinOnefis checkinOnefis);
    }
}

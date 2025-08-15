using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CheckinOnefisService : ICheckinOnefisService
    {
        private readonly ICheckinOnefisRepository _repository;

        public CheckinOnefisService(ICheckinOnefisRepository repository)
        {
            _repository = repository;
        }

        public Task<List<CheckinOnefis>> GetAllAsync() => _repository.GetAllAsync();

        public Task<CheckinOnefis> GetByAttendanceCodeAsync(string code) => _repository.GetByAttendanceCodeAsync(code);

        public Task UpsertAsync(CheckinOnefis checkinOnefis) => _repository.UpsertAsync(checkinOnefis);
    }
}

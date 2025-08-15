using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DailyInspectionService : IDailyInspectionService
    {
        private readonly IDailyInspectionRepository _repository;

        public DailyInspectionService(IDailyInspectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DailyInspection>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DailyInspection> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> CreateAsync(DailyInspection inspection)
        {
            return await _repository.InsertAsync(inspection);
        }

        public async Task<bool> UpdateAsync(string id, DailyInspection inspection)
        {
            return await _repository.UpdateAsync(id, inspection);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

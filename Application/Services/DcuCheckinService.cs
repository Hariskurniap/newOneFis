using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DcuCheckinService : IDcuCheckinService
    {
        private readonly IDcuCheckinRepository _repository;

        public DcuCheckinService(IDcuCheckinRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<DcuCheckin>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> ModifyAsync(string id, DcuCheckin data)
        {
            if (data.Status == "unused")
            {
                return await _repository.UpdateAsync(id, data);
            }
            else if (data.Status == "used")
            {
                return await _repository.InsertAsync(data);
            }
            return false;
        }

        public async Task<bool> InsertAsync(DcuCheckin data)
        {
            return await _repository.InsertAsync(data);
        }

        public async Task<DcuCheckin> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}

using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Application.Services
{
    public class CheckinService : ICheckinService
    {
        private readonly ICheckinRepository _repository;

        public CheckinService(ICheckinRepository repository)
        {
            _repository = repository;
        }

        //public IQueryable<Checkin> GetAllQueryable()
        //{
        //    return _repository.GetAllQueryable();
        //}

        public Task<List<Checkin>> GetAllAsync() => _repository.GetAllAsync();

        public async Task<Checkin> GetByAmtEmployeeIdAndEmptyCheckoutAsync(string amtEmployeeId)
        {
            return await _repository.GetByAmtEmployeeIdAndEmptyCheckoutAsync(amtEmployeeId);
        }

        public async Task UpsertAsync(Checkin checkin)
        {
            await _repository.UpsertAsync(checkin);
        }

        public async Task<(bool Success, string Message)> ModifyInsertAsync(IEnumerable<Checkin> datas)
        {
            try
            {
                foreach (var item in datas)
                {
                    item.Id = string.IsNullOrEmpty(item.Id) ? ObjectId.GenerateNewId().ToString() : item.Id;
                    item.CreatedAt ??= DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                    item.UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

                    await _repository.UpsertAsync(item);
                }

                return (true, "Insert successful.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> ModifyUpdateAsync(IEnumerable<Checkin> datas)
        {
            try
            {
                foreach (var item in datas)
                {
                    if (string.IsNullOrEmpty(item.Id))
                        return (false, "Checkin Id is required for update.");

                    item.UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                    await _repository.UpsertAsync(item);
                }

                return (true, "Update successful.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}

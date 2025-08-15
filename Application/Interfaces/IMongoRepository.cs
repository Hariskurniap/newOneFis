using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMongoRepository
    {
        Task<bool> CheckDOExistAsync(string deliveryNumber, string plant);
        Task InsertListDOAsync(ListDO listDO);
        Task InsertManyAsync(IEnumerable<ListDO> listDOs);
        Task<bool> ModifyListDOAsync(string deliveryNumber, string plant, ListDO updatedDO);
    }
}

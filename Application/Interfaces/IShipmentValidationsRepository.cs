using Domain.Entities;
using System.Linq;

namespace Application.Interfaces
{
    public interface IShipmentValidationsRepository
    {
        IQueryable<ShipmentValidations> GetAll();
        ShipmentValidations GetById(string id);
        void Create(ShipmentValidations entity);
        void Update(string id, ShipmentValidations entity);
    }
}

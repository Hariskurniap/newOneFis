using Domain.Entities;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IShipmentValidationsService
    {
        IEnumerable<ShipmentValidations> GetAll();
        ShipmentValidations GetById(string id);
        void Create(ShipmentValidations entity);
        void Update(string id, ShipmentValidations entity);
    }
}

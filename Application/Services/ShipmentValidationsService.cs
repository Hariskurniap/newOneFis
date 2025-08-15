using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services
{
    public class ShipmentValidationsService : IShipmentValidationsService
    {
        private readonly IShipmentValidationsRepository _repository;

        public ShipmentValidationsService(IShipmentValidationsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ShipmentValidations> GetAll() => _repository.GetAll();

        public ShipmentValidations GetById(string id) => _repository.GetById(id);

        public void Create(ShipmentValidations entity) => _repository.Create(entity);

        public void Update(string id, ShipmentValidations entity) => _repository.Update(id, entity);
    }
}

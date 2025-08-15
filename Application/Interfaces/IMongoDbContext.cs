using Domain.Entities;
using MongoDB.Driver;

namespace Application.Interfaces;

public interface IMongoDbContext
{
    IMongoDatabase Database { get; }
    IMongoCollection<ListDO> ListDOCollection { get; }
    IMongoCollection<Shipment> ShipmentCollection { get; }
    IMongoCollection<Checkin> CheckinCollection { get;  }
}

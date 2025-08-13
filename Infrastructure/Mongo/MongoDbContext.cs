using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Mongo;

public class MongoDbContext : IMongoDbContext
{
    public IMongoDatabase Database { get; }

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration["Mongo:Connection"];
        var databaseName = configuration["Mongo:Database"];
        var client = new MongoClient(connectionString);
        Database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<ListDO> ListDOCollection =>
        Database.GetCollection<ListDO>("ListDO");
}

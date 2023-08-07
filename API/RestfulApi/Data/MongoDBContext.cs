using MongoDB.Driver;
using RestfulApi.Entities;

namespace RestfulApi.Data;
public sealed class MongoDBContext
{
    private readonly IMongoDatabase _database;
    public MongoDBContext(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("MongoDB");
        string? databaseName = configuration.GetConnectionString("DatabaseName");

        MongoClient client = new(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>("Customer");
}
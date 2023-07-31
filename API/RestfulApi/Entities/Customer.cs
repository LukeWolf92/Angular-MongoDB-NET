using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestfulApi.Entities;

public sealed class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = null;
    public string IDcustomer { get; set; }
    public string CompanyName { get;set; }
    public string Address { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public int? SubscriptionState { get; set; } = 0;
}

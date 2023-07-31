using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestfulApi.Entities;

public sealed class Invoice
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string IDcustomer { get; set; }
    public string InvoiceNumber { get;set; }
    public DateTime Date { get; set; }
    public double Total { get; set; }
}

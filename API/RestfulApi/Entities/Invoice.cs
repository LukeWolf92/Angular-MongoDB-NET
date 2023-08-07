using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestfulApi.Entities;

public sealed class Invoice
{
    public string InvoiceNumber { get;set; }
    public DateTime Date { get; set; }
    public double Total { get; set; }
}

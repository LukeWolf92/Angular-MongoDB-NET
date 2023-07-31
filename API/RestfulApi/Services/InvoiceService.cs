using MongoDB.Driver;
using RestfulApi.Entities;
using RestfulApi.Data;

namespace RestfulApi.Services;

public interface IInvoice
{
    #region Invoices
    List<Invoice> GetInvoices(string IDcustomer = "");
    void SaveInvoice(Invoice invoice);
    DeleteResult DeleteInvoice(string id);
    #endregion
}
public sealed class InvoiceService : IInvoice
{
    private readonly MongoDBContext _mongoContext;
    public InvoiceService(MongoDBContext mongoContext) => _mongoContext = mongoContext;

    #region Invoices
    public List<Invoice> GetInvoices(string IDcustomer = "") => !string.IsNullOrEmpty(IDcustomer)
            ? _mongoContext.Invoices.Find(x => x.IDcustomer == IDcustomer).ToList()
            : _mongoContext.Invoices.Find(_ => true).ToList();
    public void SaveInvoice(Invoice invoice)
    {
        if (string.IsNullOrWhiteSpace(invoice.Id))
            _mongoContext.Invoices.InsertOne(invoice);
        else
            _mongoContext.Invoices.ReplaceOne(x => x.Id == invoice.Id, invoice);
    }
    public DeleteResult DeleteInvoice(string id) => _mongoContext.Invoices.DeleteOne(x => x.Id == id);
    #endregion
}

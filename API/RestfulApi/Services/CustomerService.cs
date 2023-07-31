using MongoDB.Driver;
using RestfulApi.Entities;
using RestfulApi.Data;

namespace RestfulApi.Services;

public interface ICustomer
{
    #region Customers
    List<Customer> GetCustomers();
    Customer GetCustomer(string IDcustomer = "");
    void SaveCustomer(Customer customer);
    DeleteResult DeleteCustomer(string id);
    #endregion
}
public sealed class CustomerService : ICustomer
{
    private readonly MongoDBContext _mongoContext;
    public CustomerService(MongoDBContext mongoContext) => _mongoContext = mongoContext;

    public List<Customer> GetCustomers() => _mongoContext.Customers.Find(_ => true).ToList();
    public Customer GetCustomer(string ID = "") => !string.IsNullOrEmpty(ID)
            ? _mongoContext.Customers.Find(x => x.Id == ID).FirstOrDefault()
            : _mongoContext.Customers.Find(_ => true).FirstOrDefault();
    public void SaveCustomer(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.Id))
            _mongoContext.Customers.InsertOne(customer);
        else
            _mongoContext.Customers.ReplaceOne(x => x.Id == customer.Id,customer);
    }
    public DeleteResult DeleteCustomer(string id) => _mongoContext.Customers.DeleteOne(x => x.Id == id);
}

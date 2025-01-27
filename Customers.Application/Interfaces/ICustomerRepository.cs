using Customers.Domain.Entities;

namespace Customers.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetByNameAsync(string name);
        Task<bool> Exists(string name);
        Task<IList<Customer>> GetAllCustomersAsync();
        Customer Add(Customer customer);
        Task<Customer> FindById(int id);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
    }
}

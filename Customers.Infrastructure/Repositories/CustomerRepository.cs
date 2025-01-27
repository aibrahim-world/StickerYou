using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        private IQueryable<Customer> GetCustomersQuery()
        {
            return _context.Customers
                .Where(c => !c.IsDeleted);
        }
        public async Task<List<Customer>> GetByNameAsync(string name)
        {
            return await GetCustomersQuery()
                .Include(c => c.Address)
                .AsNoTracking()
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }
        public async Task<bool> Exists(string name)
        {
            return await GetCustomersQuery()
                .AsNoTracking()
                .AnyAsync(c => c.Name.Contains(name));
        }

        public async Task<IList<Customer>> GetAllCustomersAsync()
        {
            return await GetCustomersQuery()
                .Include(c => c.Address)
                .ToListAsync();
        }
        public Customer Add(Customer customer)
        {
            _context.Customers.Add(customer);
            return customer;
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                customer.IsDeleted = true;
            }
        }

        public async Task<Customer> FindById(int id)
        {
            return await GetCustomersQuery()
                .Include(a => a.Address)
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == id);
        }
        public async Task UpdateAsync(Customer customer)
        {
            var existingCustomer = await GetCustomersQuery()
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == customer.Id);
            if (existingCustomer != null && !existingCustomer.IsDeleted)
            {
                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
                _context.Entry(existingCustomer).Property(x => x.AddressId).IsModified = false;
                if (customer.Address != null)
                {
                    customer.Address.Id = existingCustomer.Address.Id;
                    _context.Entry(existingCustomer.Address).CurrentValues.SetValues(customer.Address);

                }
            }
        }
    }
}

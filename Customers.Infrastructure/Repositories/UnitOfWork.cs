using Customers.Application.Interfaces;


namespace Customers.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerDbContext _context;
        private ICustomerRepository _customerRepository;
        private ICustomerEventLogRepository _customerEventLogRepository;
        private IAddressRepository _addressRepository;

        public UnitOfWork(CustomerDbContext context)
        {
            _context = context;
        }
        public ICustomerRepository Customers => _customerRepository ??= new CustomerRepository(_context);
        public ICustomerEventLogRepository CustomerEventLog => _customerEventLogRepository ??= new CustomerEventLogRepository(_context);
        public IAddressRepository Addresses => _addressRepository ?? new AddressRepository(_context);
        
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

using Customers.Application.Interfaces;
using Customers.Domain.Entities;


namespace Customers.Infrastructure.Repositories
{
    internal class AddressRepository : IAddressRepository
    {
        private readonly CustomerDbContext _context;
        public AddressRepository(CustomerDbContext context)
        {
            _context = context;
        }
        public Address AddAddress(Address address)
        {
            _context.Addresses.Add(address);
            return address;
        }
        public void UpdateAddress(Address address)
        {
            _context.Addresses.Update(address);
        }
    }
}

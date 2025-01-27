using Customers.Domain.Entities;


namespace Customers.Application.Interfaces
{
    public interface IAddressRepository
    {
        Address AddAddress(Address address);

        void UpdateAddress(Address address);
    }
}

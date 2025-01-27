

namespace Customers.Application.Interfaces
{
    public interface IUnitOfWork
    {

        ICustomerRepository Customers { get; }
        ICustomerEventLogRepository CustomerEventLog { get; }
        IAddressRepository Addresses { get; }

        Task<int> SaveChangesAsync();
    }
}

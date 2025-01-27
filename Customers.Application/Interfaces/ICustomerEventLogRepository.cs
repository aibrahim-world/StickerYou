using Customers.Domain.Entities;
using Customers.Domain.Enums;

namespace Customers.Application.Interfaces
{
    public interface ICustomerEventLogRepository
    {
        CustomerEventLog AddCustomerEventLog(int customerId, EventType eventType);
    }
}

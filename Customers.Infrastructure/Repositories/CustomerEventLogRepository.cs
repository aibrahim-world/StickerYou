using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.Enums;

namespace Customers.Infrastructure.Repositories
{
    internal class CustomerEventLogRepository: ICustomerEventLogRepository
    {
        private readonly CustomerDbContext _context;
        public CustomerEventLogRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public CustomerEventLog AddCustomerEventLog(int customerId, EventType eventType)
        {
            var customerEventLog = new CustomerEventLog
            {
                CustomerId = customerId,
                EventType = eventType,
                EventDateTime = DateTimeOffset.UtcNow
            };

            _context.CustomerEventLogs.Add(customerEventLog);

            return customerEventLog;
        }
    }
}

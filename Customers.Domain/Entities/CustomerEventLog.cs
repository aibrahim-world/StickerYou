using Customers.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Customers.Domain.Entities
{
    public class CustomerEventLog
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public EventType EventType { get; set; }
        public DateTimeOffset EventDateTime { get; set; }

        
    }
}

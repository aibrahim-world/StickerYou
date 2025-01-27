using Customers.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Customers.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int AddressId { get; set; }
        public Category CategoryId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual Address Address { get; set; }
        public virtual ICollection<CustomerEventLog> CustomerEventLogs { get; set; } = new List<CustomerEventLog>();
    }
}

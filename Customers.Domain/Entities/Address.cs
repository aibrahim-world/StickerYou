using Customers.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Customers.Domain.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int HouseNumber { get; set; }
        public string AddressLine1 { get; set; }
        public State StateId { get; set; }
        public Country CountryId { get; set; }

    }
}

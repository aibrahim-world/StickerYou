using Customers.Application.DTOs;
using Customers.Domain.Entities;


namespace Customers.Application.Extensions
{
    internal static class CustomerDtoExtensions
    {
        public static Customer GetCustomerEntity(this CustomerDto customer)
        {
            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                DateOfBirth = customer.DateOfBirth,
                Gender = customer.Gender,
                CategoryId = customer.CategoryId,
            };
        }
        public static Address GetAddressEntity(this CustomerDto customer)
        {
            return new Address
            {
                HouseNumber = customer.HouseNumber,
                AddressLine1 = customer.AddressLine1,
                StateId = customer.State,
                CountryId = customer.Country,
            };
        }

    }
}

using Customers.Application.DTOs;
using Customers.Application.Extensions;
using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.Enums;

namespace Customers.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ILogger<CustomerService> _logger;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateCustomerAsync(CustomerDto customerDto)
        {
            var customer = customerDto.GetCustomerEntity();
            var address = _unitOfWork.Addresses.AddAddress(customerDto.GetAddressEntity());
            customer.Address = address;
            customer.CustomerEventLogs.Add(new CustomerEventLog
            {
                CustomerId = customer.Id,
                EventType = EventType.CustomerCreated,
                EventDateTime = DateTime.UtcNow

            });
            _unitOfWork.Customers.Add(customer);
        }
        private bool ValidateCustomerDto(CustomerDto customerDto)
        {
            if (string.IsNullOrWhiteSpace(customerDto.Name) ||
                string.IsNullOrWhiteSpace(customerDto.AddressLine1) ||
                customerDto.HouseNumber <= 0 ||
                customerDto.DateOfBirth == default)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UploadCustomerAsync(List<CustomerDto> customerDtos)
        {
            foreach (var customerDto in customerDtos)
            {
                if (!ValidateCustomerDto(customerDto))
                {
                    // log invalid customer
                    continue;
                }
                //check if customer already exists, use name as identifier
                if (await _unitOfWork.Customers.Exists(customerDto.Name))
                {
                    // log Exists
                    continue;
                }
                CreateCustomerAsync(customerDto);
            }

            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            await _unitOfWork.Customers.DeleteAsync(id);
            _unitOfWork.CustomerEventLog.AddCustomerEventLog(
                id,
                EventType.CustomerDeleted
                );
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateCustomerAsync(int id, string name)
        {
            var result = 0;
            var customer = await _unitOfWork.Customers.FindById(id);
            if (customer != null)
            {
                customer.Name = name;
                await _unitOfWork.Customers.UpdateAsync(customer);
                _unitOfWork.CustomerEventLog.AddCustomerEventLog(
                    id,
                    EventType.CustomerUpdated
                );
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result > 0;
        }
        public async Task<bool> UpdateCustomerAsync(CustomerDto customerDto)
        {
            if (customerDto.Id <= 0)
                return false;
            var address = customerDto.GetAddressEntity();
            var customer = customerDto.GetCustomerEntity();
            customer.Address = address;

            await _unitOfWork.Customers.UpdateAsync(customer);

            _unitOfWork.CustomerEventLog.AddCustomerEventLog(
                customerDto.Id,
                EventType.CustomerUpdated
            );
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IList<CustomerDto>> GetCustomerByName(string name)
        {
            var customer = await _unitOfWork.Customers.GetByNameAsync(name);
            return new List<CustomerDto>(customer.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                AddressLine1 = c.Address.AddressLine1,
                HouseNumber = c.Address.HouseNumber,
                State = (State)c.Address.StateId,
                Country = (Country)c.Address.CountryId,
                CategoryId = (Category)c.CategoryId,
                DateOfBirth = c.DateOfBirth,
                Gender = c.Gender

            }));
        }
    }
}

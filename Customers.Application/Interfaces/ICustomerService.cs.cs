using Customers.Application.DTOs;

namespace Customers.Application.Interfaces
{
    public interface ICustomerService
    {

        Task<bool> UploadCustomerAsync(List<CustomerDto> uploadCustomerDtos);
        Task<bool> UpdateCustomerAsync(CustomerDto customerDto);
        Task<bool> UpdateCustomerAsync(int id, string name);
        Task<bool> DeleteCustomerAsync(int id);
        Task<IList<CustomerDto>> GetCustomerByName(string name);
    }
}

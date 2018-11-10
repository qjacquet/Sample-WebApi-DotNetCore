namespace Sample.BusinessService.CustomerService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dtos;

    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomer(CustomerDto customer);

        Task<CustomerDto> FetchCustomerById(int id);

        Task<IEnumerable<CustomerDto>> FetchAllCustomers();

        Task<bool> UpdateCustomer(CustomerDto customer);

    }
}

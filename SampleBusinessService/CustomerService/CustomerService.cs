namespace Sample.BusinessService.CustomerService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dal.DataAccess;
    using DalModels;
    using Dtos;
    using Dtos.DtoExtensions;

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> CreateCustomer(CustomerDto customer)
        {
            if (!CustomerIsValid(customer))
            {
                return null;
            }

            var newCustomer = await _customerRepository.CreateCustomer(customer.ToCustomer());

            return newCustomer?.ToDto();
        }

        public async Task<CustomerDto> FetchCustomerById(int id)
        {
            var customer = await _customerRepository.FetchCustomerById(id);

            return customer?.ToDto();
        }

        public async Task<IEnumerable<CustomerDto>> FetchAllCustomers()
        {

            var customerList = await _customerRepository.FetchAllCustomers();

            return customerList.Select(customer => customer.ToDto()).ToList();
        }

        public async Task<bool> UpdateCustomer(CustomerDto customer)
        {
            if (!CustomerIsValid(customer))
            {
                return false;
            }

            var existingCustomer = await _customerRepository.FetchCustomerById(customer.Id);

            if (existingCustomer is null)
            {
                return false;
            }

            OverloadExistingCustomer(existingCustomer, customer.ToCustomer());

            return await _customerRepository.UpdateCustomer(existingCustomer);
        }


        // Helper Functions

        private static void OverloadExistingCustomer(Customer existingCustomer, Customer updatedCustomer)
        {
            existingCustomer.EmailAddress = updatedCustomer.EmailAddress;
            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.Surname = updatedCustomer.Surname;
            existingCustomer.Password = updatedCustomer.Password;
        }

        private bool CustomerIsValid(CustomerDto customer)
        {
            if (customer is null)
            {
                return false;
            }

            return true;
        }
    }
}

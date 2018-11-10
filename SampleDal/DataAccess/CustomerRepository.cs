namespace Sample.Dal.DataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DalModels;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDataOperations _dataOperations;

        public CustomerRepository(IDataOperations dataOperations)
        {
            _dataOperations = dataOperations;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            return _dataOperations.CreateOrUpdateCustomerRecord(customer) ? customer : null;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            return _dataOperations.CreateOrUpdateCustomerRecord(customer);
        }

        public async Task<Customer> FetchCustomerById(int id)
        {
            return _dataOperations.FetchExistingCustomer(id);
        }

        public async Task<IEnumerable<Customer>> FetchAllCustomers()
        {
            return _dataOperations.FetchAllCustomers();
        }
    }
}

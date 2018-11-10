namespace Sample.Dal.DataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DalModels;

    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomer(Customer customer);

        Task<bool> UpdateCustomer(Customer customer);

        Task<Customer> FetchCustomerById(int id);

        Task<IEnumerable<Customer>> FetchAllCustomers();
    }
}

namespace Sample.Dal.DataAccess
{
    using System.Collections.Generic;
    using DalModels;

    public interface IDataOperations
    {
        bool CreateOrUpdateCustomerRecord(Customer customer);

        Customer FetchExistingCustomer(int customerId);

        IEnumerable<Customer> FetchAllCustomers();
    }
}

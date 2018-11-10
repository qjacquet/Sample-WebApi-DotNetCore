namespace CustomerRepositoryTests
{
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Newtonsoft.Json;
    using System.IO;
    using Sample.Dal.DataAccess;
    using Sample.DalModels;
    using Xunit;

    public class CustomerRepositoryShould
    {
        [Fact]
        public void Return_Null_If_Create_Fails()
        {
            var mockDataOperations = new Mock<IDataOperations>();
            mockDataOperations.Setup(d => d.CreateOrUpdateCustomerRecord(It.IsAny<Customer>())).Returns(false);

            var customer = new Customer
            {
                Id = 0,
                FirstName = "TEST",
                Surname = "TEST",
                EmailAddress = "TEST",
                Password = "TEST"
            };

            var customerRepository = new CustomerRepository(mockDataOperations.Object);

            var sut = customerRepository.CreateCustomer(customer);

            Assert.Null(sut.Result);
        }


    }
}

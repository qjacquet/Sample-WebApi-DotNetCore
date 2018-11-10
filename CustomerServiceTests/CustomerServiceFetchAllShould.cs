namespace CustomerServiceTests
{
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sample.DalModels;
    using Sample.Dtos;
    using Xunit;

    public class CustomerServiceFetchAllShould : CustomerServiceShouldBase
    {

        [Fact]
        public async Task Return_Empty_When_No_Records_Exist()
        {
            MockCustomerRepo.Setup(c => c.FetchAllCustomers()).ReturnsAsync(new List<Customer>());

            var result = await CustomerServiceSut.FetchAllCustomers();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Return_Customer_List_When_Records_Exist()
        {
            MockCustomerRepo.Setup(c => c.FetchAllCustomers()).ReturnsAsync(ListOfCustomers());

            var result = CustomerServiceSut.FetchAllCustomers();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Task<IEnumerable<CustomerDto>>>(result);
        }

        private static IEnumerable<Customer> ListOfCustomers()
        {
            return new List<Customer>
            {
                CreateCustomer("1"),
                CreateCustomer("2"),
                CreateCustomer("3")
            };
        }

        private static Customer CreateCustomer(string number)
        {
            return new Customer
            {
                FirstName = $"TestFirstName{number}",
                Surname = $"TestSurname{number}",
                EmailAddress = $"TestEmailAddress{number}",
                Password = $"TestPassword{number}"
            };
        }
    }
}

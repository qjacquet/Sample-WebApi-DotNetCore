namespace CustomerServiceTests
{
    using Moq;
    using Sample.DalModels;
    using Xunit;

    public class CustomerServiceUpdateShould : CustomerServiceShouldBase
    {
        [Fact]
        public void Return_False_When_Customer_Null()
        {
            var result = CustomerServiceSut.UpdateCustomer(null);

            Assert.False(result.Result);
        }

        [Fact]
        public void Return_False_When_Empty_EmailAddress()
        {
            var testCustomer = CreateTestCustomerDto();
            testCustomer.EmailAddress = string.Empty;

            var result = CustomerServiceSut.UpdateCustomer(testCustomer);

            Assert.False(result.Result);
        }

        [Fact]
        public void Return_False_When_NonExisting_EmailAddress()
        {
            var testCustomer = CreateTestCustomerDto();

            MockCustomerRepo.Setup(c => c.FetchCustomerById(It.IsAny<int>())).ReturnsAsync(() => null);

            var result = CustomerServiceSut.UpdateCustomer(testCustomer);

            Assert.False(result.Result);
        }


        [Fact]
        public void Return_False_When_Update_Fails()
        {
            var testCustomer = CreateTestCustomerDto();
            MockCustomerRepo.Setup(c => c.FetchCustomerById(It.IsAny<int>())).ReturnsAsync(new Customer());
            MockCustomerRepo.Setup(c => c.UpdateCustomer(It.IsAny<Customer>())).ReturnsAsync(false);

            var result = CustomerServiceSut.UpdateCustomer(testCustomer);

            Assert.False(result.Result);
        }

        [Fact]
        public void Return_False_When_Update_Succeeds()
        {
            var testCustomer = CreateTestCustomerDto();
            MockCustomerRepo.Setup(c => c.FetchCustomerById(It.IsAny<int>())).ReturnsAsync(new Customer());
            MockCustomerRepo.Setup(c => c.UpdateCustomer(It.IsAny<Customer>())).ReturnsAsync(true);

            var result = CustomerServiceSut.UpdateCustomer(testCustomer);

            Assert.True(result.Result);
        }
    }
}

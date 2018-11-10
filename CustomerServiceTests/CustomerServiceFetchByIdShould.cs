namespace CustomerServiceTests
{
    using Moq;
    using Sample.DalModels;
    using Sample.Dtos;
    using Xunit;

    public class CustomerServiceFetchByIdShould : CustomerServiceShouldBase
    {
        [Fact]
        public void Return_Null_When_Id_Zero()
        {
            var result = CustomerServiceSut.FetchCustomerById(0);

            Assert.Null(result.Result);
        }

        [Fact]
        public void Return_CustomerDto_When_Id_Supplied()
        {
            MockCustomerRepo.Setup(c => c.FetchCustomerById(It.IsAny<int>()))
                .ReturnsAsync(CreateNewCustomer());

            var result = CustomerServiceSut.FetchCustomerById(12);

            Assert.NotNull(result.Result);
            Assert.IsType<CustomerDto>(result.Result);
            Assert.Equal(12, result.Result.Id);
        }

        private static Customer CreateNewCustomer()
        {
            return new Customer
            {
                Id = 12,
                FirstName = "TestFirstName",
                Surname = "TestSurName",
                EmailAddress = "TestEmailAddress",
                Password = "TestPassword"
            };
        } 
    }
}

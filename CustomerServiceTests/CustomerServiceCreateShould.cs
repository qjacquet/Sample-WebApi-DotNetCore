namespace CustomerServiceTests
{
    using Moq;
    using Sample.DalModels;
    using Sample.Dtos.DtoExtensions;
    using Xunit;

    public class CustomerServiceCreateShould : CustomerServiceShouldBase
    {
        [Fact]
        public void Return_Null_When_Null_CustomerDto()
        {
            var result = CustomerServiceSut.CreateCustomer(null);

            Assert.Null(result.Result);
        }

        [Fact]
        public void Return_Null_When_Customer_Create_Fails()
        {
            var testCustomerDto = CreateTestCustomerDto();
            MockCustomerRepo.Setup(c => c.CreateCustomer(It.IsAny<Customer>())).ReturnsAsync(() => null);

            var result = CustomerServiceSut.CreateCustomer(testCustomerDto);

            Assert.Null(result.Result);
        }

        [Fact]
        public void Return_Customer_When_Customer_Create_Succeeds()
        {
            var testCustomerDto = CreateTestCustomerDto();
            var existingCustomerDto = CreateTestCustomerDto();
            existingCustomerDto.Id = 12;

            MockCustomerRepo.Setup(c => c.CreateCustomer(It.IsAny<Customer>())).ReturnsAsync(existingCustomerDto.ToCustomer());

            var result = CustomerServiceSut.CreateCustomer(testCustomerDto);

            Assert.NotNull(result.Result);
            Assert.Equal(12, result.Result.Id);
        }
    }
}

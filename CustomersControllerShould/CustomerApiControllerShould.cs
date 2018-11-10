namespace CustomersApiControllerTests
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using Sample.BusinessService.CustomerService;
    using Sample.Dtos;
    using Sample.WebApi.Controllers;
    using Xunit;

    public class CustomerApiControllerShould
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomersApiController _customerApiControllerSut;

        public CustomerApiControllerShould()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _customerApiControllerSut = new CustomersApiController(_mockCustomerService.Object);
        }

        [Fact]
        public void Return_OK_With_CustomerDto_List_When_GetAll_Called()
        {
            _mockCustomerService.Setup(c => c.FetchAllCustomers()).ReturnsAsync(new List<CustomerDto>());
            var result = _customerApiControllerSut.Get();

            Assert.NotNull(result);
            var resultValue = result.Result as OkObjectResult;
            Assert.NotNull(resultValue);
            var resultsValueList = resultValue.Value as List<CustomerDto>;
            Assert.Equal(new List<CustomerDto>(), resultsValueList);
        }

        [Fact]
        public void Return_Ok_With_CustomerDto_When_GetById_Called()
        {
            var testCustomerDto = CreateCustomerDto();
            _mockCustomerService.Setup(c => c.FetchCustomerById(It.IsAny<int>())).ReturnsAsync(testCustomerDto);
            var result = _customerApiControllerSut.GetById(1);

            Assert.NotNull(result);
            var resultValue = result.Result as OkObjectResult;
            Assert.NotNull(resultValue);
            var resultsValue = resultValue.Value as CustomerDto;
            Assert.Equal(testCustomerDto, resultsValue);
        }

        [Fact]
        public void Return_Bad_Request_When_Create_Customer_Fails_On_Post()
        {
            var testCustomerDto = CreateCustomerDto();
            _mockCustomerService.Setup(c => c.CreateCustomer(It.IsAny<CustomerDto>())).ReturnsAsync(() => null);

            var result = _customerApiControllerSut.Post(testCustomerDto);

            Assert.NotNull(result);
            var resultValue = result.Result as BadRequestResult;
            Assert.NotNull(resultValue);
        }

        [Fact]
        public void Return_Created_When_Create_Customer_Succeeds_On_Post()
        {
            var testCustomerDto = CreateCustomerDto();
            _mockCustomerService.Setup(c => c.CreateCustomer(It.IsAny<CustomerDto>())).ReturnsAsync(testCustomerDto);

            var testInputCustomerDto = CreateCustomerDto();
            testInputCustomerDto.Id = 0;
            var result = _customerApiControllerSut.Post(testInputCustomerDto);

            Assert.NotNull(result);
            var resultValue = result.Result as CreatedAtActionResult;
            Assert.NotNull(resultValue);
            var resultsValue = resultValue.Value as CustomerDto;
            Assert.Equal(testCustomerDto, resultsValue);
        }

        [Fact]
        public void Return_BadRequest_When_Route_Id_Not_Match_Customer_Id_On_Put()
        {
            var testInputCustomerDto = CreateCustomerDto();
            testInputCustomerDto.Id = 2;
            var result = _customerApiControllerSut.Put(1, testInputCustomerDto);

            Assert.NotNull(result);
            var resultValue = result.Result as BadRequestResult;
            Assert.NotNull(resultValue);
        }

        [Fact]
        public void Return_BadRequest_When_UpdateCustomer_Fails_On_Put()
        {
            _mockCustomerService.Setup(c => c.UpdateCustomer(It.IsAny<CustomerDto>())).ReturnsAsync(false);

            var testInputCustomerDto = CreateCustomerDto();
            var result = _customerApiControllerSut.Put(1, testInputCustomerDto);

            Assert.NotNull(result);
            var resultValue = result.Result as BadRequestResult;
            Assert.NotNull(resultValue);
        }

        [Fact]
        public void Return_Ok_When_UpdateCustomer_Succeeds_On_Put()
        {
            _mockCustomerService.Setup(c => c.UpdateCustomer(It.IsAny<CustomerDto>())).ReturnsAsync(true);

            var testInputCustomerDto = CreateCustomerDto();
            var result = _customerApiControllerSut.Put(1, testInputCustomerDto);

            Assert.NotNull(result);
            var resultValue = result.Result as OkResult;
            Assert.NotNull(resultValue);
        }

        private static CustomerDto CreateCustomerDto()
        {
            return new CustomerDto
            {
                Id = 1,
                FirstName = "test1",
                Surname = "test1",
                EmailAddress = "testemail@1.com",
                Password = "test1"
            };
        }
    }
}

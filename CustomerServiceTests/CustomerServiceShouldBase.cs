namespace CustomerServiceTests
{
    using Moq;
    using Sample.BusinessService.CustomerService;
    using Sample.Dal.DataAccess;
    using Sample.Dtos;

    public class CustomerServiceShouldBase
    {
        public CustomerService CustomerServiceSut;
        public Mock<ICustomerRepository> MockCustomerRepo;

        public CustomerServiceShouldBase()
        {

            MockCustomerRepo = new Mock<ICustomerRepository>();
            CustomerServiceSut = new CustomerService(MockCustomerRepo.Object);
        }

        public CustomerDto CreateTestCustomerDto()
        {
            return new CustomerDto
            {
                FirstName = "TestFirstname",
                Surname = "TestSurName",
                EmailAddress = "TestEmailAddress",
                Password = "TestPassword"
            };
        }

    }
}

namespace Sample.Dtos.DtoExtensions
{
    using DalModels;

    public static class CustomerDtoExtensions
    {
        public static Customer ToCustomer(this CustomerDto customerDto)
        {
            return new Customer
            {
                Id = customerDto.Id,
                FirstName = customerDto.FirstName,
                Surname = customerDto.Surname,
                EmailAddress = customerDto.EmailAddress,
                Password = customerDto.Password
            };
        }

        public static CustomerDto ToDto(this Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                Surname = customer.Surname,
                EmailAddress = customer.EmailAddress,
                Password = customer.Password
            };
        }
    }
}

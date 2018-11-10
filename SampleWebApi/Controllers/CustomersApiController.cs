namespace Sample.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BusinessService.CustomerService;
    using Dtos;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/customers")]
    public class CustomersApiController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersApiController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/customers
        [HttpGet("")]
        [ProducesResponseType(typeof(List<CustomerDto>), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _customerService.FetchAllCustomers());
        }

        // GET api/customers
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _customerService.FetchCustomerById(id));
        }

        // POST api/customers
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] CustomerDto customer)
        {
            var newCustomer = await _customerService.CreateCustomer(customer);

            if (newCustomer is null)
            {
                return BadRequest();
            }

            return CreatedAtAction("Post", newCustomer);
        }

        // PUT api/customers/1
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerDto customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var updateSuccess = await _customerService.UpdateCustomer(customer);

            if (!updateSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeniaHeroApiV3.Business.Abstract;
using ZeniaHeroApiV3.Business.Request.Customer;
using ZeniaHeroApiV3.Business.Response.Customer;
using ZeniaHeroApiV3.Data.Customer;

namespace ZeniaHeroApiV3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static List<Customer> customers = new();
        private readonly ILogger<CustomerController> _logger;
        private ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger,ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegistirationRequest customer)
        {
            _logger.LogInformation("Register Method is triggered.");
            var result=customers.FindAll(c=>c.Username==customer.Username);
            if(result.Count()>=1) { return BadRequest("Username is already exist"); }
            _customerService.CreatePasswordHash(customer.Password,out byte[] passwordHash,out byte[] passwordSalt);
            Customer registeredCustomer = new()
            {
                CompanyId = customer.CompanyId,
                Username = customer.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            customers.Add(registeredCustomer);
            return Ok(registeredCustomer);

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(CustomerLoginRequest customer)
        {
            _logger.LogInformation("Login Method is triggered.");
            var result=customers.FindAll(f=>f.Username==customer.Username);
            if (result.Count == 0) { return BadRequest("Customer is not found."); }
            foreach(var customerIn in result)
            {
                if (!_customerService.VerifyPasswordHash(customer.Password, customerIn.PasswordHash, customerIn.PasswordSalt)) return BadRequest("wrong info");
                string token = _customerService.CreateToken(customerIn);
                CustomerLoginResponse loginResponse = new() { CompanyId = customerIn.CompanyId, Username = customerIn.Username, Password = token };

                return Ok(loginResponse);
                
            }
            return Ok();
        }
        
    }
}

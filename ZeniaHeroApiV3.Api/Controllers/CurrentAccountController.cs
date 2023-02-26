using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeniaHeroApiV3.Business.Response.CurrentAccount;
using ZeniaHeroApiV3.Data.CurrentAccount;

namespace ZeniaHeroApiV3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentAccountController : ControllerBase
    {
        private static List<CurrentAccount> currentAccounts = new();
        private readonly ILogger<CurrentAccountController> _logger;
        public CurrentAccountController(ILogger<CurrentAccountController> logger)
        {
            _logger = logger;
        }
        [HttpGet("Multiple")]
        public async Task<IActionResult> GetCurrentAccounts([FromQuery] string registirationNo,int caseCode)
        {
            _logger.LogInformation("GetCurrentAccounts Method is triggered.");
            List<CurrentAccountResponse> responseList=new List<CurrentAccountResponse>();
            var accounts=currentAccounts.FindAll(f=>f.PersonalIdentityNo.Contains(registirationNo)&&f.CaseCode.ToString().Contains(caseCode.ToString()));
            foreach (var account in accounts)
            {
                CurrentAccountResponse response = new()
                {
                    No = account.No,
                    Name = account.Name,
                    TaxRegistrationNo = account.TaxRegistrationNo,
                    PersonalIdentityNo = account.PersonalIdentityNo,
                    TaxAreaCode = account.TaxAreaCode,
                    Address = account.Address,
                    City = account.City,
                    Country = account.Country,
                    PhoneNo = account.PhoneNo,
                };
                responseList.Add(response);
            }
            return Ok(responseList);
        }
        [HttpGet("Single")]
        public async Task<IActionResult> GetCurrentAccount([FromQuery]string registirationNo,int caseCode)
        {
            _logger.LogInformation("GetCurrentAccount Method is triggered.");
            var account = currentAccounts.Find(f => f.PersonalIdentityNo == registirationNo && f.CaseCode == caseCode);
            return Ok(account);
        }
    }
}

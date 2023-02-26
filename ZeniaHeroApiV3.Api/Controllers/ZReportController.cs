using AutoFixture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeniaHeroApiV3.Business.Response.ZReport;
using ZeniaHeroApiV3.Data.ZReport;

namespace ZeniaHeroApiV3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ZReportController : ControllerBase
    {
        private static List<ZReport> reports = new();
        private readonly ILogger<ZReportController> _logger;
        public ZReportController(ILogger<ZReportController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetZReportBySalesDate([FromQuery] DateTime salesDate)
        {
            Data();
            _logger.LogInformation("GetZReportBySalesDate Method is triggered.");
            var companyId = await GetCurrentCustomerCompanyId();
            List<ZReportResponse> responseList=new List<ZReportResponse>();
            var reportsList=reports.FindAll(f=>f.CompanyId==companyId&&f.SalesDate==salesDate);
            if (reportsList.Count == 0) { return BadRequest("SalesReport is not found"); }
            foreach(var report in reportsList)
            {
                ZReportResponse response = new()
                {
                    Id = report.Id,
                    CompanyId = report.CompanyId,
                    StoreCode = report.StoreCode,
                    BusinessDate = report.BusinessDate,
                    SalePosId = report.SalePosId,
                    CashierId = report.CashierId,
                    CashierName = report.CashierName,
                    TenderDetailType = report.TenderDetailType,
                    TenderDetailName = report.TenderDetailName,
                    CashierAmount = report.CashierAmount,
                    CashierDifference = report.CashierDifference,
                    RefundAmount = report.RefundAmount,
                    RefundCount = report.RefundCount,
                    TotalGrossSales = report.TotalGrossSales,
                    TotalNetSales = report.TotalNetSales,
                    DiscountAmount = report.DiscountAmount,
                    TotalTaxAmt = report.TotalTaxAmt,
                    InvoiceCount = report.InvoiceCount,
                    ReceiptCount = report.ReceiptCount,
                    ProductCount = report.ProductCount,
                };
                responseList.Add(response);
            }
            return Ok(responseList);

        }
        private async Task<string> GetCurrentCustomerCompanyId()
        {
            var companyIdClaim = User.FindFirst("CompanyId");
            string companyId = companyIdClaim.Value;
            return companyId;
        }
        private void Data()
        {
            var fixture = new Fixture();
            ZReport zReport1=fixture.Create<ZReport>();
            ZReport zReport2=fixture.Create<ZReport>();
            ZReport zReport3=fixture.Create<ZReport>();
            reports.Add(zReport1);
            reports.Add(zReport2);
            reports.Add(zReport3);
        }
    }
}

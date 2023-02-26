using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeniaHeroApiV3.Business.Abstract;
using ZeniaHeroApiV3.Business.Response.SalesReport;
using ZeniaHeroApiV3.Data.SalesReport;

namespace ZeniaHeroApiV3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesReportController : ControllerBase
    {
        private static List<SalesReport> salesReports = new();
        private readonly ILogger<SalesReportController> _logger;
        private readonly ISalesReportService _salesReportService;
        public SalesReportController(ILogger<SalesReportController> logger,ISalesReportService salesReportService)
        {
            _logger = logger;
            _salesReportService = salesReportService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSalesReportBySalesDate([FromQuery] DateTime salesDate)
        {
            Data();
            _logger.LogInformation("GetSalesReportBySalesDate Method is triggered.");
            var companyId = await GetCurrentCustomerCompanyId();
            List<SalesReportResponse> responseList = new List<SalesReportResponse>();
            var reportsList=salesReports.FindAll(f=>f.SalesDate==salesDate&&f.CompanyId==companyId);
            if(reportsList.Count==0) { return BadRequest("SalesReport is not found"); }
            foreach(var report in reportsList)
            {
                SalesReportResponse response = new()
                {
                    Id = report.Id,
                    CompanyId = report.CompanyId,
                    StoreCode = report.StoreCode,
                    BusinessDate = report.BusinessDate,
                    OrderKey = report.OrderKey,
                    SalePosId = report.SalePosId,
                    SaleOperatorId = report.SaleOperatorId,
                    SaleOperatorName = report.SaleOperatorName,
                    ProductName = report.ProductName,
                    ProductCode = report.ProductCode,
                    TenderDetailType = report.TenderDetailType,
                    TenderDetailName = report.TenderDetailName,
                    TotalGrossSales = report.TotalGrossSales,
                    TotalNetSales = report.TotalNetSales,
                    TaxFormulaCode = report.TaxFormulaCode,
                    TaxAmt = report.TaxAmt,
                    IsRefund = report.IsRefund,
                    RefundAmount = report.RefundAmount,
                    DiscountAmount = report.DiscountAmount
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
            SalesReport salesReport1 = new()
            {
                Id = 1,
                SalesDate = new DateTime(2020,10,04),
                CompanyId = "2",
                StoreCode = "3020",
                BusinessDate = new DateTime(2020,10,04),
                OrderKey = "3020020027120201004",
                SalePosId = "2",
                SaleOperatorId = "158251",
                SaleOperatorName = "berhan sümeli",
                ProductName = "KETCAP",
                ProductCode = "90006",
                TenderDetailType = "62",
                TenderDetailName = "Garanti",
                TotalGrossSales = 0,
                TotalNetSales = 0,
                TaxFormulaCode = "",
                TaxAmt = 0,
                IsRefund = false,
                RefundAmount = 0,
                DiscountAmount = 0,
            };
            SalesReport salesReport2 = new()
            {
                Id = 2,
                SalesDate = new DateTime(2018,06,06),
                CompanyId = "4",
                StoreCode = "2030",
                BusinessDate = new DateTime(2019,06,01),
                OrderKey = "2030003001233213274",
                SalePosId = "2",
                SaleOperatorId = "158251",
                SaleOperatorName = "berhan sümeli",
                ProductName = "MAYONEZ",
                ProductCode = "90032",
                TenderDetailType = "62",
                TenderDetailName = "Garanti",
                TotalGrossSales = 0,
                TotalNetSales = 0,
                TaxFormulaCode = "",
                TaxAmt = 0,
                IsRefund = false,
                RefundAmount = 0,

                DiscountAmount = 0,
            };

            salesReports.Add(salesReport1);
            salesReports.Add(salesReport2);
        }
    }
}

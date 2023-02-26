using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeniaHeroApiV3.Business.Response.SalesReport
{
    public class SalesReportResponse
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string StoreCode { get; set; }
        public DateTime BusinessDate { get; set; }
        public string OrderKey { get; set; }
        public string SalePosId { get; set; }
        public string SaleOperatorId { get; set; }
        public string SaleOperatorName { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string TenderDetailType { get; set; }
        public string TenderDetailName { get; set; }
        public decimal TotalGrossSales { get; set; }
        public decimal TotalNetSales { get; set; }
        public string TaxFormulaCode { get; set; }
        public decimal TaxAmt { get; set; }
        public bool IsRefund { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}

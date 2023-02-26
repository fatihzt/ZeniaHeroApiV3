using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeniaHeroApiV3.Data.ZReport
{
    public class ZReport
    {
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }
        public string CompanyId { get; set; }
        public string StoreCode { get; set; }
        public DateTime BusinessDate { get; set; }
        public string SalePosId { get; set; }
        public string CashierId { get; set; }
        public string CashierName { get; set; }
        public string TenderDetailType { get; set; }
        public string TenderDetailName { get; set; }
        public decimal CashierAmount { get; set; }
        public decimal CashierDifference { get; set; }
        public decimal RefundAmount { get; set; }
        public int RefundCount { get; set; }
        public int ReceiptCount { get; set; }
        public int ProductCount { get; set; }
        public decimal TotalGrossSales { get; set; }
        public decimal TotalNetSales { get; set; }
        public decimal DiscountAmount { get; set; }
        public string TotalTaxAmt { get; set; }
        public string InvoiceCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeniaHeroApiV3.Data.CurrentAccount
{
    public class CurrentAccount
    {
        public int Id { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string TaxRegistrationNo { get; set; }
        public int CaseCode { get; set; }
        public string PersonalIdentityNo { get; set; }
        public string TaxAreaCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeniaHeroApiV3.Data.Customer
{
    
    public  class Customer
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
    
    


}

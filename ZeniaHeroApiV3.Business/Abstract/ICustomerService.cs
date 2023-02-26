using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeniaHeroApiV3.Data.Customer;

namespace ZeniaHeroApiV3.Business.Abstract
{
    public interface ICustomerService
    {
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        string CreateToken(Customer customer);
        
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZeniaHeroApiV3.Business.Abstract;
using ZeniaHeroApiV3.Data.Customer;

namespace ZeniaHeroApiV3.Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private List<Customer> customers = new();
        private readonly IConfiguration _configuration;
        public CustomerManager(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null || password.Length < 9) { throw new ArgumentException("Password must be at least 8 characters long"); }
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateToken(Customer customer)
        {
            var secureKey = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);
            var claims = new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Sub,_configuration.GetSection("jwt:Subject").Value),
                            new Claim("Username",customer.Username),
                            new Claim("PasswordHash",customer.PasswordHash.ToString()),
                            new Claim("PasswordSalt",customer.PasswordSalt.ToString()),
                            new Claim("CompanyId",customer.CompanyId),
                        };

            var securityKey = new SymmetricSecurityKey(secureKey);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);


            var token = new JwtSecurityToken(
                _configuration.GetSection("Jwt:Issuer").Value,
                _configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddMinutes(10)

                );

            var tokens = new JwtSecurityTokenHandler().WriteToken(token);

            return tokens;
        }

        

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public void Data(List<Customer> customers)
        {
            throw new NotImplementedException();
        }
    }
}

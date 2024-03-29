using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interface;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUsers appUsers)
        {
            var claims=new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,appUsers.UserName)

            };

            var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var tokendescriptor=new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials=creds
            };
            
            var tokenhandler=new JwtSecurityTokenHandler();

            var token=tokenhandler.CreateToken(tokendescriptor);

            return tokenhandler.WriteToken(token);



        }
    }
}
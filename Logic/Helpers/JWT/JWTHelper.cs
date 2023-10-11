using Data.Entities;
using Logic.Models.Token;
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

namespace Logic.Helpers.JWT
{
    public class JWTHelper
    {
        public TokenModel CreateToken(IConfiguration configuration, Member member, string role)
        {
            var key = Encoding.ASCII.GetBytes(configuration["JWTToken:Key"]!);
            var audience = configuration["JWTToken:Audience"];
            var issuer = configuration["JWTToken:Issuer"];
            var tokenhandler = new JwtSecurityTokenHandler();
            var ToeknDescp = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, member.UserName!),
                    new Claim(ClaimTypes.NameIdentifier, member.Id),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Email, member.Email)  
                }),
                Audience = audience,
                Issuer = issuer,
                Expires = DateTime.Now.AddHours(1),
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };



            var TokenCreate = tokenhandler.CreateToken(ToeknDescp);
            var accessToken = tokenhandler.WriteToken(TokenCreate);
            var refreshToken = RefreashTokenCreator();

            return new TokenModel(accessToken, refreshToken);
        }

        public string RefreashTokenCreator()
        {
            byte[] bytes = new byte[33];
            using (var key = RandomNumberGenerator.Create())
            {
                key.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}

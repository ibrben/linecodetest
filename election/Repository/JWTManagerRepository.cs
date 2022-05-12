using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using election.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace election.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration configuration;
        private IUsersRepository usersRepository;
        
        public JWTManagerRepository(IConfiguration configuration, IUsersRepository usersRepository)
        {
            this.configuration = configuration;
            this.usersRepository = usersRepository;
        }

        Tokens IJWTManagerRepository.Authenticate(Users users)
        {
            if (!usersRepository.Verify(users))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var tokenLifetime = Int32.Parse(configuration["JWT:SessionPeriod"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, users.user) }),
                Expires = DateTime.UtcNow.AddMinutes(tokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { token = tokenHandler.WriteToken(token) };

        }
    }
}

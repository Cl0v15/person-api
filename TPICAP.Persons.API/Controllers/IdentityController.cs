using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TPICAP.Persons.API.Core;

namespace TPICAP.Persons.API.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly JwtConfiguration _jWTConfiguration;
        private readonly IConfiguration _configuration;

        public IdentityController(
            JwtConfiguration jWTConfiguration,
            IConfiguration configuration)
        {
            _jWTConfiguration = jWTConfiguration;
            _configuration = configuration;
        }

        /// <summary>
        /// Quick implementation of JWT generation for testing purposes
        /// </summary>
        /// <returns>JWT</returns>
        [HttpGet]
        public IActionResult SignIn()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, "username"),
            };

            var notBeforeDateTime = string.IsNullOrEmpty(_jWTConfiguration.TokenExpiresNotBefore)
                 ? DateTime.UtcNow
                 : DateTime.ParseExact(_jWTConfiguration.TokenExpiresNotBefore, "yyMMddHHmm", CultureInfo.InvariantCulture);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value)),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                 issuer: _jWTConfiguration.Issuer,
                 audience: _jWTConfiguration.Audience,
                 claims: claims,
                 expires: DateTime.UtcNow.AddDays(_jWTConfiguration.TokenExpiresInDays),
                 notBefore: notBeforeDateTime,
                 signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(token);
        }
    }
}

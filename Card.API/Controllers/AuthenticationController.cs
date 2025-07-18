using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CityInfo.API.Entities;

namespace CityInfo.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICardRepository _cityInfoRepository;

        // we won't use this outside of this class, so we can scope it to this namespace
        public class AuthenticationRequestBody
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        public AuthenticationController(IConfiguration configuration, ICardRepository cityInfoRepository)
        {
            _configuration = configuration ?? 
                throw new ArgumentNullException(nameof(configuration));
            _cityInfoRepository = cityInfoRepository ??
            throw new ArgumentNullException(nameof(cityInfoRepository));
      }

        [HttpPost("authenticate")]
        public  ActionResult<string> Authenticate(
            AuthenticationRequestBody authenticationRequestBody)
        {  
            // Step 1: validate the username/password
            var user = ValidateUserCredentials(
                authenticationRequestBody.Email,
                authenticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // Step 2: create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);
             
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("userid", user.Id.ToString()));
            claimsForToken.Add(new Claim("role", user.Role));
             
            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private User ValidateUserCredentials(string? userName, string? password)
        {
           return  _cityInfoRepository.GetCurrentUserAsync(userName, password).Result;
        }
    }
}

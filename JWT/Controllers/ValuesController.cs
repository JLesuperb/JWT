using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IConfiguration _config;

        public ValuesController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]User _User)
        {
            _User = new User { UserId = 15, FirstName = "FirstName", LastName = "LastName", UserName=_User.UserName };
            String TokenString = BuildToken(_User);
            return Ok(new { Token = TokenString });
            //return Ok(_User);
        }

        private string BuildToken(User _User)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> ListOfClaims = new List<Claim>
            {
                new Claim("UserName", _User.UserName)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              ListOfClaims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

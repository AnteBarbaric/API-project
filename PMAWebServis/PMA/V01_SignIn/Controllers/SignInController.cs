using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Models;
using Services;
using Queries.Accounts;
using Models.Tables;

namespace V01_SignIn.Controllers
{
    [EnableCors("Policy3")]
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        protected readonly IUserSignInManager signMng;
        public SignInController(IUserSignInManager signMng)
        {
            this.signMng = signMng;
        }
        // GET: api/<SignInController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Kolekcija koju vraća http akcija GET", "Ovaj RESTful web servis podržava http akcije GET i POST" };
        }

        // GET api/<SignInController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return string.Format("Pozvana je metoda Get({0}) putem http akcije GET i url: ~/api/signin/{0}", id);
        }

        // POST api/<SignInController>
        [EnableCors("Policy3")]
        [HttpPost]
        public IActionResult Post([FromBody] UserLoginModel model)
        {
            ResponseModel response = new ResponseModel();
            if (this.signMng.GetSignIn(model))
            {
                response.Code = 200;
                response.Message = "Krisnik je uspješno prijavljen u sustav";
                return Ok(response);
            }
            else
            {
                response.Code = 400;
                response.Message = "Korisničko ime ili lozinka nisu ispravni. Pokušajte ponovo";
                return BadRequest(response);
            }
        }

        // PUT api/<SignInController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok($"Pozvana je metoda Put({id}, {value})");
        }

        // DELETE api/<SignInController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Pozvana je metoda Delete({id})");
        }
    }
}

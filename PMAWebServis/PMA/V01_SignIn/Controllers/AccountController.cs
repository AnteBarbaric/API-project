using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using Services;
using Models;
using Models.Accounts;
using Models.Tables;
using Queries.Accounts;

namespace V01_SignIn.Controllers
{
    [EnableCors("Policy3")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        protected IAccountManager accountManager;

        public AccountController(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IActionResult Get()
        {
            // ona će vraćati pozivaču popis svih korisničkih računa iz tablice UserLogin baze podataka
            OperationResult<AccountListModel> result = this.accountManager.GetAll();

            if(result.Succeeded)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //vratit će podatke jednog korisničkoga računa iz tablice UserLogin baze podataka i to onaj kojem je UserLoginId = id ili grešku BadRequest ako taj ne postoji u tablici
            OperationResult<AccountModel> result = this.accountManager.Get(id);

            if(result.Succeeded)
            {
                return Ok(result.Result);
            }
            else
            {
                return NotFound(result.ToString());
            }
        }

        // POST api/<AccountController>
        [HttpPost]
        public IActionResult Post([FromBody] AccountModel model)
        {
            //ova metoda prima podatke novog korisničkog računa i kreira novi korisnički račun u tablici UserLogin
            OperationResult<ResponseModel> result = this.accountManager.Create(model);

            if(result.Succeeded)
            {
                result.Result.Code = StatusCodes.Status200OK;
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AccountModel model)
        {
            OperationResult<ResponseModel> result = this.accountManager.Update(id, model);

            if (result.Succeeded)
            {
                result.Result.Code = StatusCodes.Status200OK;
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        // PUT api/<AccountController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] AccountPatchModel model)
        {
            OperationResult<ResponseModel> result = this.accountManager.Change(id, model);

            if (result.Succeeded)
            {
                result.Result.Code = StatusCodes.Status200OK;
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            OperationResult<ResponseModel> result = this.accountManager.Delete(id);

            if (result.Succeeded)
            {
                result.Result.Code = StatusCodes.Status200OK;
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(result.ToString());
            }
        }

        // POST api/<AccountController>/table
        [EnableCors("Policy3")]
        [HttpPost("table")]
        public IActionResult TablePost([FromBody] AccountTableInputModel model) //
        {

            AccountQuery query = new AccountQuery(model);

            if (query.RunQuery())
            {
                query.Result.Code = StatusCodes.Status200OK;
                query.Result.ElapsedMs = query.ElapsedMs;
                return Ok(query.Result);
            }
            else
            {
                return BadRequest(query.Error);
            }
        }
    }
}

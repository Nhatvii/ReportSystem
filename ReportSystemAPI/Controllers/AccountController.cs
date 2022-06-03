using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportSystemData.Models;
using ReportSystemData.Parameters;
using ReportSystemData.Service;
using ReportSystemData.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportSystemAPI.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _repository;

        public AccountController(IAccountService service)
        {
            _repository = service;
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<Account> GetAllAccount()
        {
            return Ok(_repository.GetAllAccount());
        }

        [HttpGet("{email}")]
        [Produces("application/json")]
        public ActionResult<Report> GetAccountByID(string email)
        {
            return Ok(_repository.GetAccountByID(email));
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<Account> Login(LoginPara login)
        {
            return Ok(_repository.Login(login));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(CreateAccountViewModel acc)
        {
            return Ok(await _repository.RegisterAsync(acc));
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportSystemData.Dtos.User;
using ReportSystemData.Models;
using ReportSystemData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportSystemAPI.Controllers
{
    //[Route("api/User")]
    //[ApiController]
    //public class UserController : ControllerBase
    //{
    //    private readonly UserService _repository;

    //    public UserController(_24HReportSystemContext context)
    //    {
    //        _repository = new UserService(context);
    //    }

        //[HttpGet]
        //[Produces("application/json")]
        //public ActionResult<User> GetAllUser()
        //{
        //    return Ok(_repository.GetAllUser());
        //}

        //[HttpPost]
        //[Route("Login")]
        //public ActionResult Login(string Email, string Password)
        //{
        //    var result = _repository.Login(Email, Password);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return NotFound("Invalid username or password");
        //    }
        //}

        //[HttpPost]
        //[Route("Register")]
        //public ActionResult Register(CreateUserDTO createUser)
        //{
        //    var result = _repository.Register(createUser);
        //    if (result)
        //    {
        //        return Ok("Create Success!!!");
        //    }
        //    return NotFound("Create Error!!!");
        //}
    //}
}

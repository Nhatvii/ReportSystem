using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportSystemData.Models;
using ReportSystemData.Parameters;
using ReportSystemData.Service;
using ReportSystemData.ViewModel.Task;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportSystemAPI.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _repository;
        public TaskController(ITaskService service)
        {
            _repository = service;
        }
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<Task> GetAllTAsk([FromQuery] TaskParameters taskParameters)
        {
            return Ok(_repository.GetAllTask(taskParameters));
        }
        [HttpGet("{id}")]
        [Produces("application/json")]
        public ActionResult<Task> GetTaskByID(string id)
        {
            return Ok(_repository.GetTaskByID(id));
        }
        [HttpPost]
        [Produces("application/json")]
        public ActionResult<Task> CreateTask(CreateTaskViewModel task)
        {
            return Ok(_repository.CreateTask(task));
        }
        [HttpDelete]
        [Produces("application/json")]
        public ActionResult DeleteTask(string id)
        {
            return Ok(_repository.DeleteTask(id));
        }
        [HttpPut]
        [Produces("application/json")]
        [Route("StatusUpdate")]
        public ActionResult ChangeTaskStatus(string id, int status)
        {
            return Ok(_repository.ChangeTaskStatus(id, status));
        }
    }
}

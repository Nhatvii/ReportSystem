using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportSystemData.Models;
using ReportSystemData.Parameter.Report;
using ReportSystemData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportSystemAPI.Controllers
{
    [Route("api/Report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _repository;
        public ReportController(IReportService service)
        {
            _repository = service;
        }
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<Report> GetAllReport()
        {
            return Ok(_repository.GetAllReport());
        }
        [HttpPost]
        [Produces("application/json")]
        public ActionResult CreateReport(CreateReportViewModel report)
        {
            return Ok(_repository.CreateReport(report));
        }
        [HttpGet("{id}")]
        [Produces("application/json")]
        public ActionResult<Report> GetReportByID(int id)
        {
            return Ok(_repository.GetAllReport());
        }
    }
}

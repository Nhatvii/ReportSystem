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
        public async Task<IActionResult> CreateReport(CreateReportViewModel report)
        {
            return Ok(await _repository.CreateReportAsync(report));
        }
        [HttpGet("{id}")]
        [Produces("application/json")]
        public ActionResult<Report> GetReportByID(string id)
        {
            return Ok(_repository.GetReportByID(id));
        }
    }
}

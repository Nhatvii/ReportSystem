using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportSystemData.Constants;
using ReportSystemData.Models;
using ReportSystemData.Parameter.Report;
using ReportSystemData.Parameters;
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
        public ActionResult<Report> GetAllReport([FromQuery] ReportParameters reportParameters)
        {
            return Ok(_repository.GetAllReport(reportParameters));
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

        /// <summary>
        /// Update Report Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        [Route("StatusUpdate")]
        public ActionResult ChangeStatus(string id, int status)
        {
            return Ok(_repository.ChangeReportStatus(id, status));
        }

        //[HttpPut]
        //[Produces("application/json")]
        //public ActionResult<Report> UpdateReport(UpdateReportViewModel report)
        //{
        //    return Ok(_repository.UpdateReport(report));
        //}
        [HttpDelete]
        [Produces("application/json")]
        public ActionResult DeleteReport(string id)
        {
            return Ok(_repository.DeleteReport(id));
        }
    }
}

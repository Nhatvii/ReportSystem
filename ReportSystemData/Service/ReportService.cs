using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
using ReportSystemData.Dtos.Report;
using ReportSystemData.Models;
using ReportSystemData.Parameter.Report;
using ReportSystemData.Repository;
using ReportSystemData.Response;
using ReportSystemData.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReportSystemData.Service
{
    public partial interface IReportService : IBaseService<Report>
    {
        Task<Report> CreateReportAsync(CreateReportViewModel report);
        List<ReportDTO> GetAllReport();
        Report UpdateReport(UpdateReportViewModel report);
        List<Report> GetReportByID(string id);
    }
    public partial class ReportService : BaseService<Report>, IReportService
    {
        private readonly IMapper _mapper;
        public ReportService(DbContext context, IMapper mapper, IReportRepository repository) : base(context, repository)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<Report> CreateReportAsync(CreateReportViewModel report)
        {
            var reportTmp = _mapper.Map<Report>(report);
            reportTmp.ReportId = Guid.NewGuid().ToString();
            reportTmp.CreateTime = DateTime.Now;
            reportTmp.Status = ReportConstants.STATUS_REPORT_NEW;
            reportTmp.UserId = "nhatvi@gmail.com";
            reportTmp.CategoryId = "1";
            await CreateAsyn(reportTmp);
            return reportTmp;
        }

        public List<ReportDTO> GetAllReport()
        {
            //var reports = _context.Report.ProjectTo<ReportDTO>(_mapper.ConfigurationProvider).ToList();
            var reports = Get().ProjectTo<ReportDTO>(_mapper.ConfigurationProvider).ToList();
            return reports;
        }

        public Report UpdateReport(UpdateReportViewModel report)
        {
            return null;
        }

        public List<Report> GetReportByID(string id)
        {
            var report = Get().Where(r => r.ReportId == id).ToList();
            return report;
        }
    }
}

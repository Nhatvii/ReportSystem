using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
using ReportSystemData.Dtos.Report;
using ReportSystemData.Models;
using ReportSystemData.Parameter.Report;
using ReportSystemData.Parameters;
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
        List<ReportDTO> GetAllReport(ReportParameters reportParameters);
        Report UpdateReport(UpdateReportViewModel report);
        List<Report> GetReportByID(string id);
        public Report ChangeReportStatus(string id, int status);
        ReportDTO DeleteReport(string id);
    }
    public partial class ReportService : BaseService<Report>, IReportService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        public ReportService(DbContext context, IMapper mapper, IReportRepository repository,
            ICategoryService categoryService, IAccountService accountService) : base(context, repository)
        {
            _dbContext = context;
            _mapper = mapper;
            _categoryService = categoryService;
            _accountService = accountService;
        }
        public List<ReportDTO> GetAllReport(ReportParameters reportParameters)
        {
            var reports = Get().Where(r => r.IsDelete == false)
                .ProjectTo<ReportDTO>(_mapper.ConfigurationProvider).ToList();
            if(reportParameters.status.HasValue && reportParameters.status > 0)
            {
                reports = GetListReportWithStatus(reportParameters.status);
            }
            return reports;
        }

        public List<ReportDTO> GetListReportWithStatus(int? status)
        {
            var report = new List<ReportDTO>();
            if(status == 1)
            {
                report = Get().Where(r => r.IsDelete == false && r.Status.Equals(ReportConstants.STATUS_REPORT_NEW))
                        .ProjectTo<ReportDTO>(_mapper.ConfigurationProvider).ToList();
            }
            if (status == 2)
            {
                report = Get().Where(r => r.IsDelete == false && r.Status.Equals(ReportConstants.STATUS_REPORT_PENDING))
                        .ProjectTo<ReportDTO>(_mapper.ConfigurationProvider).ToList();
            }
            if (status == 3)
            {
                report = Get().Where(r => r.IsDelete == false && r.Status.Equals(ReportConstants.STATUS_REPORT_APPROVE))
                        .ProjectTo<ReportDTO>(_mapper.ConfigurationProvider).ToList();
            }
            if (status == 4)
            {
                report = Get().Where(r => r.IsDelete == false && r.Status.Equals(ReportConstants.STATUS_REPORT_DENIED))
                        .ProjectTo<ReportDTO>(_mapper.ConfigurationProvider).ToList();
            }
            return report;
        }
        public async Task<Report> CreateReportAsync(CreateReportViewModel report)
        {
            var account = _accountService.GetAccountByID(report.UserID);
            if(account != null)
            {
                if(account.RoleId == 1)
                {
                    var reportTmp = _mapper.Map<Report>(report);
                    reportTmp.ReportId = Guid.NewGuid().ToString();
                    reportTmp.CreateTime = DateTime.Now;
                    reportTmp.Status = ReportConstants.STATUS_REPORT_NEW;
                    reportTmp.UserId = report.UserID;
                    reportTmp.CategoryId = 1;
                    await CreateAsyn(reportTmp);
                    return reportTmp;
                }
                throw new ErrorResponse("Only User account can create report!!!", (int)HttpStatusCode.NoContent);
            }
            throw new ErrorResponse("Unavailable Account!!!", (int)HttpStatusCode.NotFound);
        }


        public Report UpdateReport(UpdateReportViewModel report)
        {
            var checkCate = _categoryService.CheckAvailableCategory(report.CategoryId);
            if(checkCate)
            {
                var reportTmp = Get().Where(rp => rp.ReportId.Equals(report.ReportId)).FirstOrDefault();
                if(reportTmp != null)
                {
                    if(report.Location != null)
                    {
                    reportTmp.Location = report.Location;
                    }
                    if(report.TimeFraud != null)
                    {
                    reportTmp.TimeFraud = report.TimeFraud;
                    }
                    if(report.Description != null)
                    {
                    reportTmp.Description = report.Description;
                    }
                    if(report.Video != null)
                    {
                    reportTmp.Video = report.Video;
                    }
                    if(report.Image != null)
                    {
                    reportTmp.Image = report.Image;
                    }
                    reportTmp.CategoryId = report.CategoryId;

                    Update(reportTmp);
                    return reportTmp;
                }
                throw new ErrorResponse("Report isn't available!", (int)HttpStatusCode.NotFound);
            }
            throw new ErrorResponse("CategoryID isn't available!", (int)HttpStatusCode.NotFound);
        }

        public List<Report> GetReportByID(string id)
        {
            var report = Get().Where(r => r.ReportId == id).ToList();
            return report;
        }

        public Report ChangeReportStatus(string id, int status)
        {
            var report = Get().Where(r => r.ReportId.Equals(id)).FirstOrDefault();
            if (report != null)
            {
                if(status == 1)
                {
                    report.Status = ReportConstants.STATUS_REPORT_NEW;
                }
                if(status == 2)
                {
                    report.Status = ReportConstants.STATUS_REPORT_PENDING;
                }
                if (status == 3)
                {
                    report.Status = ReportConstants.STATUS_REPORT_APPROVE;
                }
                if (status == 4)
                {
                    report.Status = ReportConstants.STATUS_REPORT_DENIED;
                }
                Update(report);
                return report;
            }
            throw new ErrorResponse("Report isn't available!", (int)HttpStatusCode.NotFound);
        }

        public ReportDTO DeleteReport(string id)
        {
            var report = Get().Where(r => r.ReportId.Equals(id)).FirstOrDefault();
            if (report != null)
            {
                report.IsDelete = true;
                throw new ErrorResponse("Delete success", (int)HttpStatusCode.OK);
            }
            throw new ErrorResponse("Report isn't available!", (int)HttpStatusCode.NotFound);
        }
    }
}

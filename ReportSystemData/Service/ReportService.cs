using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
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
        Task<SuccessResponse> CreateReportAsync(CreateReportViewModel report);
        List<Report> GetAllReport(ReportParameters reportParameters);
        SuccessResponse UpdateReport(UpdateReportViewModel report);
        Report GetReportByID(string id);
        public SuccessResponse ChangeReportStatus(string id, int status, string staffID);
        SuccessResponse DeleteReport(string id);
        SuccessResponse ChangeReportCategory(string id, int categoryID, string staffID);
        SuccessResponse UpdateReportEditor(string reportID, string editorID);
    }
    public partial class ReportService : BaseService<Report>, IReportService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly IReportDetailService _reportDetailService;
        public ReportService(DbContext context, IMapper mapper, IReportRepository repository,
            ICategoryService categoryService, IAccountService accountService, IReportDetailService reportDetailService) : base(context, repository)
        {
            _dbContext = context;
            _mapper = mapper;
            _categoryService = categoryService;
            _accountService = accountService;
            _reportDetailService = reportDetailService;
        }
        public List<Report> GetAllReport(ReportParameters reportParameters)
        {
            var reports = Get().Where(r => r.IsDelete == false)
                .Include(detail => detail.ReportDetail)
                .Include(cate => cate.Category)
                .Include(c => c.ReportView).ToList();
            if (reportParameters.Status.HasValue && reportParameters.Status > 0)
            {
                reports = GetListReportWithStatus(reportParameters.Status);
            }
            if (reportParameters.StaffID != null)
            {
                reports = reports.Where(c => (c.StaffId != null) && c.StaffId.Equals(reportParameters.StaffID)).ToList();
            }
            return reports;
        }

        public List<Report> GetListReportWithStatus(int? status)
        {
            var report = new List<Report>();
            if (status == 1)
            {
                report = Get().Where(r => r.IsDelete == false && r.Status.Equals(ReportConstants.STATUS_REPORT_NEW)).Include(detail => detail.ReportDetail).ToList();
            }
            if (status == 2)
            {
                report = Get().Where(r => r.IsDelete == false && r.Status.Equals(ReportConstants.STATUS_REPORT_PENDING)).Include(detail => detail.ReportDetail).ToList();
            }
            if (status == 3)
            {
                report = Get().Where(r => r.IsDelete == false && r.Status.Equals(ReportConstants.STATUS_REPORT_APPROVE)).Include(detail => detail.ReportDetail).ToList();
            }
            if (status == 4)
            {
                report = Get().Where(r => r.IsDelete == false && r.Status.Equals(ReportConstants.STATUS_REPORT_DENIED)).Include(detail => detail.ReportDetail).ToList();
            }
            return report;
        }
        public async Task<SuccessResponse> CreateReportAsync(CreateReportViewModel report)
        {
            //if (report.UserID != null || !report.UserID.Equals("string"))
            //{
            //    var account = _accountService.GetAccountByID(report.UserID);
            //    if (!(account.RoleId == 1))
            //    {
            //        throw new ErrorResponse("Only User account can create report!!!", (int)HttpStatusCode.NoContent);
            //    }
            //}
            var reportTmp = _mapper.Map<Report>(report);
            reportTmp.ReportId = Guid.NewGuid().ToString();
            reportTmp.CreateTime = DateTime.Now;
            reportTmp.Status = ReportConstants.STATUS_REPORT_NEW;
            reportTmp.IsAnonymous = report.IsAnonymous;
            //if(report.userid.equals("string") || report.userid == null)
            //{
            //    reporttmp.userid = null;
            //}
            //if (!(report.UserID.Equals("string")) && !string.IsNullOrEmpty(report.UserID))
            //{
                reportTmp.UserId = report.UserID;
            //}
        //if (!(string.IsNullOrEmpty(report.UserID) || !report.UserID.Equals("string")))
        //{
        //    reportTmp.UserId = report.UserID;
        //}
        reportTmp.CategoryId = 1;
            await CreateAsyn(reportTmp);
            await _reportDetailService.CreateReportDetail(reportTmp.ReportId, report.Image, report.Video);
            return new SuccessResponse((int)HttpStatusCode.OK, "Create Success");
        }

        public SuccessResponse UpdateReport(UpdateReportViewModel report)
        {
            var checkCate = _categoryService.CheckAvailableCategory(report.CategoryId);
            if (checkCate)
            {
                var reportTmp = Get().Where(rp => rp.ReportId.Equals(report.ReportId)).FirstOrDefault();
                if (reportTmp != null)
                {
                    if (report.Location != null)
                    {
                        reportTmp.Location = report.Location;
                    }
                    if (report.TimeFraud != null)
                    {
                        reportTmp.TimeFraud = report.TimeFraud;
                    }
                    if (report.Description != null)
                    {
                        reportTmp.Description = report.Description;
                    }
                    reportTmp.CategoryId = report.CategoryId;

                    Update(reportTmp);
                    return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
                }
                throw new ErrorResponse("Report isn't available!", (int)HttpStatusCode.NotFound);
            }
            throw new ErrorResponse("CategoryID isn't available!", (int)HttpStatusCode.NotFound);
        }
        public Report GetReportByID(string id)
        {
            var report = Get().Where(r => r.ReportId == id && r.IsDelete == false)
                .Include(detail => detail.ReportDetail)
                .Include(cate => cate.Category)
                .Include(c => c.ReportView).FirstOrDefault();
            return report;
        }

        public SuccessResponse ChangeReportStatus(string id, int status, string staffID)
        {
            var report = Get().Where(r => r.ReportId.Equals(id)).FirstOrDefault();
            if (report != null)
            {
                if (status == 1)
                {
                    report.Status = ReportConstants.STATUS_REPORT_NEW;
                }
                if (status == 2)
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
                report.StaffId = staffID;
                Update(report);
                return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
            }
            throw new ErrorResponse("Report isn't available!", (int)HttpStatusCode.NotFound);
        }

        public SuccessResponse DeleteReport(string id)
        {
            var report = GetReportByID(id);
            if (report != null)
            {
                report.IsDelete = true;
                Update(report);
                return new SuccessResponse((int)HttpStatusCode.OK, "Delete Success");
            }
            throw new ErrorResponse("Report isn't available!", (int)HttpStatusCode.NotFound);
        }

        public SuccessResponse ChangeReportCategory(string id, int categoryID, string staffID)
        {
            var report = GetReportByID(id);
            if (report != null)
            {
                var checkCate = _categoryService.CheckAvailableCategory(categoryID);
                if (checkCate)
                {
                    report.CategoryId = categoryID;
                    report.StaffId = staffID;
                    Update(report);
                    return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
                }
                throw new ErrorResponse("CategoryID isn't available!", (int)HttpStatusCode.NotFound);
            }
            throw new ErrorResponse("Report isn't available!", (int)HttpStatusCode.NotFound);
        }

        public SuccessResponse UpdateReportEditor(string reportID, string editorID)
        {
            var report = GetReportByID(reportID);
            if(report != null)
            {
                report.EditorId = editorID;
                Update(report);
                return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
            }
            throw new ErrorResponse("Report isn't available!", (int)HttpStatusCode.NotFound);
        }
    }
}

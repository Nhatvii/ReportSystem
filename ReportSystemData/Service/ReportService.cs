using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        Report CreateReport(CreateReportViewModel report);
        List<ReportDTO> GetAllReport();
        Report UpdateReport(UpdateReportViewModel report);
    }
    public partial class ReportService : BaseService<Report>, IReportService
    {
        private readonly _24HReportSystemContext _context;
        private readonly IMapper _mapper;
        public ReportService(_24HReportSystemContext context, IMapper mapper, IReportRepository repository) : base(context, repository)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Report CreateReport(CreateReportViewModel report)
        {
            var AccountFlag = false;
            foreach (Account tmp in _context.Account.ToList())
            {
                if(tmp.Email.Trim().Equals(report.UserId.Trim()))
                {
                    AccountFlag = true;
                    break;
                }
            }
            var CateFlag = false;
            foreach(Category tmp in _context.Category.ToList())
            {
                if(tmp.CategoryId.Trim().Equals(report.CategoryId.Trim()))
                {
                    CateFlag = true;
                    break;
                }
            }

            if(!AccountFlag)
            {
                throw new ErrorResponse("Account not Found", (int)HttpStatusCode.NotFound);
            } else if(!CateFlag)
            {
                throw new ErrorResponse("Category not Found", (int)HttpStatusCode.NotFound);
            }
            var reportTmp = _mapper.Map<Report>(report);
            reportTmp.ReportId = Guid.NewGuid().ToString();
            reportTmp.CreateTime = DateTime.Now;
            reportTmp.Status = ReportConstants.STATUS_REPORT_NEW;
            _context.Report.Add(reportTmp);
            _context.SaveChanges();
            //await CreateAsyn(reportTmp);

            //var reports = new Report()
            //{
            //    ReportId = Guid.NewGuid().ToString(),
            //    Location = report.Location,
            //    TimeFraud = report.TimeFraud,
            //    Description = report.Description,
            //    Video = report.Video,
            //    Image = report.Image,
            //    CategoryId = report.CategoryId,
            //    CreateTime = DateTime.Now,
            //    IsAnonymous = report.IsAnonymous,
            //    UserId = report.UserId,
            //    Status = "New",
            //};
            //_context.Report.Add(reports);
            //_context.SaveChanges();
            return reportTmp;
        }

        public List<ReportDTO> GetAllReport()
        {
            var reports = _context.Report.ProjectTo<ReportDTO>(_mapper.ConfigurationProvider).ToList();
            return reports;
        }

        public Report UpdateReport(UpdateReportViewModel report)
        {
            return null;
        }
    }
}

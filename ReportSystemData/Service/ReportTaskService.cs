using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
using ReportSystemData.Models;
using ReportSystemData.Repositories;
using ReportSystemData.Response;
using ReportSystemData.Service.Base;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ReportSystemData.Service
{
    public partial interface IReportTaskService : IBaseService<ReportTask>
    {
        SuccessResponse CreateReportTask(string taskID, string reportID, DateTime? createTime);
    }
    public class ReportTaskService : BaseService<ReportTask>, IReportTaskService
    {
        public ReportTaskService(DbContext context, IReportTaskRepository repository) : base(context, repository)
        {
            _dbContext = context;
        }

        public SuccessResponse CreateReportTask(string taskID, string reportID, DateTime? createTime)
        {
            var reportTask = new ReportTask()
            {
                TaskId = taskID,
                ReportId = reportID,
                CreateTime = (DateTime)createTime,
                Status = TaskConstants.STATUS_TASK_NEW
            };
            Create(reportTask);
            return new SuccessResponse((int)HttpStatusCode.OK, "Create Success");
        }
    }
}

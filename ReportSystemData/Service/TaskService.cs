using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
using ReportSystemData.Models;
using ReportSystemData.Parameters;
using ReportSystemData.Repositories;
using ReportSystemData.Response;
using ReportSystemData.Service.Base;
using ReportSystemData.ViewModel.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ReportSystemData.Service
{
    public partial interface ITaskService : IBaseService<Task>
    {
        List<Task> GetAllTask(TaskParameters taskParameters);
        Task GetTaskByID(string id);
        SuccessResponse CreateTask(CreateTaskViewModel task);
        SuccessResponse DeleteTask(string id);
        SuccessResponse ChangeTaskStatus(string id, int status);
    }
    public partial class TaskService : BaseService<Task>, ITaskService
    {
        private readonly IReportService _reportService;
        private readonly IAccountService _accountService;
        private readonly IReportTaskService _reportTaskService;
        public TaskService(DbContext context, ITaskRepository repository,
            IReportService reportService, IAccountService accountService, IReportTaskService reportTaskService) : base(context, repository)
        {
            _dbContext = context;
            _reportService = reportService;
            _accountService = accountService;
            _reportTaskService = reportTaskService;
        }
        public List<Task> GetAllTask(TaskParameters taskParameters)
        {
            var tasks = Get().Where(c => c.IsDelete == false)
                .Include(t => t.ReportTask).ThenInclude(t => t.Report).ToList();
            if(taskParameters.EditorID != null)
            {
                tasks = tasks.Where(t => t.EditorId.Equals(taskParameters.EditorID)).ToList();
            }
            if(taskParameters.Status.HasValue && taskParameters.Status > 0)
            {
                if(taskParameters.Status == 1)
                {
                    tasks = tasks.Where(t => t.Status.Equals(TaskConstants.STATUS_TASK_NEW)).ToList();
                }
                if (taskParameters.Status == 2)
                {
                    tasks = tasks.Where(t => t.Status.Equals(TaskConstants.STATUS_TASK_PENDING)).ToList();
                }
                if (taskParameters.Status == 3)
                {
                    tasks = tasks.Where(t => t.Status.Equals(TaskConstants.STATUS_TASK_FINISH)).ToList();
                }
                if (taskParameters.Status == 4)
                {
                    tasks = tasks.Where(t => t.Status.Equals(TaskConstants.STATUS_TASK_UNFINISHED)).ToList();
                }
            }
            return tasks;
        }

        public Task GetTaskByID(string id)
        {
            var task = Get().Where(t => t.TaskId.Equals(id) && t.IsDelete == false).Include(t => t.ReportTask).ThenInclude(t => t.Report).FirstOrDefault();
            return task;
        }
        public SuccessResponse CreateTask(CreateTaskViewModel task)
        {
            foreach (var item in task.ReportId)
            {
                var check = _reportService.GetReportByID(item);
                if(check == null)
                {
                    throw new ErrorResponse("Report isn't available!", (int)HttpStatusCode.NotFound);
                }
            }
            var checkAccountRole = _accountService.GetAccountByID(task.EditorId);
            if(checkAccountRole== null)
            {
                throw new ErrorResponse("EditorId isn't available!", (int)HttpStatusCode.NotFound);
            }
            if(checkAccountRole.RoleId == 1 || checkAccountRole.RoleId == 2 || checkAccountRole.RoleId == 4 || checkAccountRole.RoleId == 5)
            {
                throw new ErrorResponse("Role account not suitable!", (int)HttpStatusCode.NotFound);
            }
            var tasks = new Task()
            {
                TaskId = Guid.NewGuid().ToString(),
                EditorId = task.EditorId,
                CreateTime = DateTime.Now,
                DeadLineTime = task.DeadlineTime,
                Status = TaskConstants.STATUS_TASK_NEW,
                IsDelete = false
            };
            Create(tasks);
            foreach (var item in task.ReportId)
            {
                _reportService.UpdateReportEditor(item, task.EditorId);
                _reportTaskService.CreateReportTask(tasks.TaskId, item, tasks.CreateTime);
            }
            return new SuccessResponse((int)HttpStatusCode.OK, "Create Success");
        }

        public SuccessResponse DeleteTask(string id)
        {
            var task = GetTaskByID(id);
            if (task != null)
            {
                task.IsDelete = true;
                Update(task);
                return new SuccessResponse((int)HttpStatusCode.OK, "Delete Success");
            }
            throw new ErrorResponse("Task isn't available!", (int)HttpStatusCode.NotFound);
        }
        public SuccessResponse ChangeTaskStatus(string id, int status)
        {
            var task = GetTaskByID(id);
            if (task != null)
            {
                if (status == 1)
                {
                    task.Status = TaskConstants.STATUS_TASK_NEW;
                }
                if (status == 2)
                {
                    task.Status = TaskConstants.STATUS_TASK_PENDING;
                }
                if (status == 3)
                {
                    task.Status = TaskConstants.STATUS_TASK_FINISH;
                }
                if (status == 4)
                {
                    task.Status = TaskConstants.STATUS_TASK_UNFINISHED;
                }
                Update(task);
                return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
            }
            throw new ErrorResponse("Task isn't available!", (int)HttpStatusCode.NotFound);
        }
    }
}

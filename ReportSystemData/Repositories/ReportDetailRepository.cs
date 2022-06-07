using Microsoft.EntityFrameworkCore;
using ReportSystemData.Models;
using ReportSystemData.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Repositories
{
    public partial interface IReportDetailRepository : IBaseRepository<ReportDetail>
    {
    }
    public partial class ReportDetailRepository : BaseRepository<ReportDetail>, IReportDetailRepository
    {
        public ReportDetailRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

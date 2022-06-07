using Microsoft.EntityFrameworkCore;
using ReportSystemData.Models;
using ReportSystemData.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Repositories
{
    public partial interface ICommentRepository : IBaseRepository<Comment>
    {
    }
    public partial class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

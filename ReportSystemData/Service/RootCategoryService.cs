using Microsoft.EntityFrameworkCore;
using ReportSystemData.Models;
using ReportSystemData.Repositories;
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
    public partial interface IRootCategoryService : IBaseService<RootCategory>
    {
        List<RootCategory> GetAllRootCategory();
        RootCategory GetRootCategoryByID(int? id);
        Task<SuccessResponse> CreateRootCategoryAsync(string rootType);
        SuccessResponse UpdateRootCategory(int id, string rootType);
        SuccessResponse DeleteRootCategory(int id);
    }
    public partial class RootCategoryService : BaseService<RootCategory>, IRootCategoryService
    {
        public RootCategoryService(DbContext context, IRootCategoryRepository repository) : base(context, repository)
        {
            _dbContext = context;
        }
        public List<RootCategory> GetAllRootCategory()
        {
            var rootCate = Get().ToList();
            return rootCate;
        }

        public RootCategory GetRootCategoryByID(int? id)
        {
            var category = Get().Where(r => r.RootCategoryId == id).FirstOrDefault();
            return category;
        }

        public async Task<SuccessResponse> CreateRootCategoryAsync(string rootType)
        {
            var rootCate = new RootCategory()
            {
                RootCategoryId = Get().Count() + 1,
                Type = rootType,
            };
            await CreateAsyn(rootCate);
            return new SuccessResponse((int)HttpStatusCode.OK, "Create Success");
        }

        public SuccessResponse UpdateRootCategory(int id, string rootType)
        {
            var rootCate = GetRootCategoryByID(id);
            if(rootCate != null)
            {
                rootCate.Type = rootType;
                Update(rootCate);
                return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
            }
            throw new ErrorResponse("Root Category isn't available", (int)HttpStatusCode.NotFound);
        }

        public SuccessResponse DeleteRootCategory(int id)
        {
            var rootCate = GetRootCategoryByID(id);
            if (rootCate != null)
            {
                Delete(rootCate);
                return new SuccessResponse((int)HttpStatusCode.OK, "Delete Success");
            }
            throw new ErrorResponse("Root Category isn't available", (int)HttpStatusCode.NotFound);
        }

    }
}

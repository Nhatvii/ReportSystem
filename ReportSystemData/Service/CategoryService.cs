using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Models;
using ReportSystemData.Repositories;
using ReportSystemData.Response;
using ReportSystemData.Service.Base;
using ReportSystemData.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReportSystemData.Service
{
    public partial interface ICategoryService : IBaseService<Category>
    {
        List<Category> GetAllCategory();
        List<Category> GetCategoryByID(int id);
        Task<SuccessResponse> CreateCategoryAsync(CreateCategoryViewModel cate);
        SuccessResponse UpdateCategory(UpdateCategoryViewModel cate);
        SuccessResponse DeleteCategory(int id);
        bool CheckAvailableCategory(int id);
    }
    public partial class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IMapper _mapper;
        public CategoryService(DbContext context, IMapper mapper, ICategoryRepository repository) : base(context, repository)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public List<Category> GetAllCategory()
        {
            var category = Get().Where(r => r.CategoryId != 1).ToList();
            return category;
        }

        public List<Category> GetCategoryByID(int id)
        {
            var category = Get().Where(r => r.CategoryId == id).ToList();
            return category;
        }

        public async Task<SuccessResponse> CreateCategoryAsync(CreateCategoryViewModel cate)
        {
            var cateTmp = _mapper.Map<Category>(cate);
            cateTmp.CategoryId = Get().Count() + 1;
            await CreateAsyn(cateTmp);
            return new SuccessResponse((int)HttpStatusCode.OK, "Create Success");
        }

        public SuccessResponse UpdateCategory(UpdateCategoryViewModel cate)
        {
            var cateTmp = Get().Where(r => r.CategoryId == cate.CategoryId).FirstOrDefault();
            cateTmp.Type = cate.Type;
            Update(cateTmp);
            return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
        }

        public SuccessResponse DeleteCategory(int id)
        {
            var cate = Get().Where(r => r.CategoryId == id).FirstOrDefault();
            if(cate != null)
            {
                Delete(cate);
                return new SuccessResponse((int)HttpStatusCode.OK, "Delete Success");
            }
            throw new ErrorResponse("Category isn't available", (int)HttpStatusCode.NotFound);
        }
        public bool CheckAvailableCategory(int id)
        {
            var cate = Get().Where(r => r.CategoryId == id).FirstOrDefault();
            if (cate != null)
            {
                return true;
            }
            return false;
        }
    }
}

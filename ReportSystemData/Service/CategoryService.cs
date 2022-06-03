using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Dtos;
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
        List<CategoryDTO> GetAllCategory();
        List<CategoryDTO> GetCategoryByID(int id);
        Task<Category> CreateCategoryAsync(CreateCategoryViewModel cate);
        Category UpdateCategory(UpdateCategoryViewModel cate);
        CategoryDTO DeleteCategory(int id);
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

        public List<CategoryDTO> GetAllCategory()
        {
            var category = Get().Where(r => r.CategoryId != 1).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();
            return category;
        }

        public List<CategoryDTO> GetCategoryByID(int id)
        {
            var category = Get().Where(r => r.CategoryId == id).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToList();
            return category;
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryViewModel cate)
        {
            var cateTmp = _mapper.Map<Category>(cate);
            cateTmp.CategoryId = Get().Count() + 1;
            await CreateAsyn(cateTmp);
            return cateTmp;
        }

        public Category UpdateCategory(UpdateCategoryViewModel cate)
        {
            var cateTmp = Get().Where(r => r.CategoryId == cate.CategoryId).FirstOrDefault();
            cateTmp.Type = cate.Type;
            Update(cateTmp);
            return cateTmp;
        }

        public CategoryDTO DeleteCategory(int id)
        {
            var cate = Get().Where(r => r.CategoryId == id).FirstOrDefault();
            if(cate != null)
            {
                Delete(cate);
                throw new ErrorResponse("Delete success", (int)HttpStatusCode.OK);
            }
            return null;
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

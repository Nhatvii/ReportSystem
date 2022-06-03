using AutoMapper;
using ReportSystemData.Dtos;
using ReportSystemData.Dtos.Post;
using ReportSystemData.Dtos.Report;
using ReportSystemData.Models;
using ReportSystemData.Parameter.Report;
using ReportSystemData.ViewModel.Category;
using ReportSystemData.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Report, ReportDTO>();
            CreateMap<Report, CreateReportViewModel>();
            CreateMap<CreateReportViewModel, Report>();

            CreateMap<Post, PostDTO>();
            CreateMap<CreatePostViewModel, Post>();

            CreateMap<Account, AccountDTO>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CreateCategoryViewModel>();
            CreateMap<CreateCategoryViewModel, Category>();

        }
    }
}

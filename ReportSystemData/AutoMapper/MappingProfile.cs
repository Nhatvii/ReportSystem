using AutoMapper;
using ReportSystemData.Models;
using ReportSystemData.Parameter.Report;
using ReportSystemData.ViewModel.Category;
using ReportSystemData.ViewModel.Comment;
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
            CreateMap<Report, CreateReportViewModel>();
            CreateMap<CreateReportViewModel, Report>();

            CreateMap<CreatePostViewModel, Post>();

            CreateMap<CreateCommentViewModel, Comment>();

            CreateMap<Category, CreateCategoryViewModel>();
            CreateMap<CreateCategoryViewModel, Category>();

        }
    }
}

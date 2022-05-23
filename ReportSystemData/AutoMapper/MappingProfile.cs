using AutoMapper;
using ReportSystemData.Dtos;
using ReportSystemData.Dtos.Post;
using ReportSystemData.Dtos.Report;
using ReportSystemData.Models;
using ReportSystemData.Parameter.Report;
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
        }
    }
}

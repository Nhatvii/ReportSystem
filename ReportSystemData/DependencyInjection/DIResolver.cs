using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReportSystemData.Models;
using ReportSystemData.Repositories;
using ReportSystemData.Repository;
using ReportSystemData.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.DependencyInjection
{
    public static class DIResolver
    {
        public static void IntializerDI(this IServiceCollection services)
        {
            services.AddScoped<DbContext, _24HReportSystemContext>();

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReportRepository, ReportRepository>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IAccountInfoService, AccountInfoService>();
            services.AddScoped<IAccountInfoRepository, AccountInfoRepository>();
        }
    }
}

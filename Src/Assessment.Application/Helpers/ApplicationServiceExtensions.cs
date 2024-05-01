using Assessment.Application.Interfaces;
using Assessment.Application.Services;
using Assessment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Assessment.Application.Helpers
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("MyDbConnection")));
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            return services;
        } 
    }
}
using FluentValidation.AspNetCore;
using Logic.Background_Services;
using Logic.Mapper;
using Logic.Services.Abstractions;
using Logic.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace Logic.Extensions
{
    public static class LogicExtensions
    {
        public static void LogicServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IGymService, GymService>();
            services.AddScoped<IPersonelService, PersonelService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddAutoMapper(typeof(LogicExtensions));
            services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<MapperProfiler>());
            services.AddLogging(options =>
            {
                options.ClearProviders();
                options.AddSerilog();
            });
            services.AddHostedService<CheckMemeberMembershipsService>();
            services.AddHostedService<CheckAppointmentService>();

        }
    }
}

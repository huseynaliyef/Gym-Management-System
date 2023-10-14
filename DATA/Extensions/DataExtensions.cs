using Data.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class DataExtensions
    {
        public static void DbInject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GymManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });


            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<GymManagementDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(k =>
            {
                k.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                k.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(p =>
            {
                var key = Encoding.ASCII.GetBytes(configuration["JWTToken:Key"]!);
                p.SaveToken = true;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JWTToken:Issuer"],
                    ValidAudience = configuration["JWTToken:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
                p.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var extype = context.Exception.GetType();
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("JWT-Expire-AccessToken", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}

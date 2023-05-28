using APLICATION.Interface;
using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina.Entities;
using DOMAIN.Canina.Setting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PERSISTENCE.Canina.Context;
using PERSISTENCE.Canina.Repository;
using PERSISTENCE.Canina.Services;
using System;
using System.Text;

namespace PERSISTENCE.Canina
{
	public static class ServiceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            service.AddIdentity<Usuarios, IdentityRole>(
                cfg =>
                {
                    cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                    cfg.SignIn.RequireConfirmedEmail = true;
                    cfg.User.RequireUniqueEmail = true;
                    cfg.Password.RequireDigit = false;
                    cfg.Password.RequiredUniqueChars = 0;
                    cfg.Password.RequireLowercase = false;
                    cfg.Password.RequireNonAlphanumeric = false;
                    cfg.Password.RequireUppercase = false;
                }
                    )
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();
            #region Repositories
            //service.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            #endregion

            //#region Caching
            //service.AddStackExchangeRedisCache(options => {
            //    options.Configuration = configuration.GetValue<string>("Caching:RedisConnection");
            //});
            //#endregion
            #region Services
            service.AddTransient<IAccountService, AccountService>();
            #endregion
            service.Configure<JWTSetting>(configuration.GetSection("JWTSetting"));
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSetting:Issuer"],
                    ValidAudience = configuration["JWTSetting:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSetting:Key"]))

                };

                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c => {

                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = context => {
                        context.HandleResponse();
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("Usted no esta autorizado!"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context => {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("Usted no tiene permiso!"));
                        return context.Response.WriteAsync(result);

                    }
                };
            });


        }
    }
}

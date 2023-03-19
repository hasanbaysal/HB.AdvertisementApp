using AutoMapper;
using FluentValidation;
using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.Business.Managers;
using HB.AdvertisementApp.Business.ValidationRules;
using HB.AdvertisementApp.Common;
using HB.AdvertisementApp.DataAccess.Contexts;
using HB.AdvertisementApp.DataAccess.UnitOfWork;
using HB.AdvertisementApp.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtention
    {
        public static void AddBusinessDependencies(this IServiceCollection services, Action<ConnectionInformation> connectionString, Assembly configFromAssembly)
        {
            #region DbContext Add to DI


            var con = new ConnectionInformation();
            connectionString(con);


            services.AddDbContext<AdvertisementAppDbContext>(opt =>
            {
                opt.UseSqlServer(con.ConnectionString);
            });
            services.AddScoped<IUow, Uow>();


            #endregion



            #region AutoMapper Profiles Add to DI
          
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), configFromAssembly);
            
            #endregion


            #region fluent validation add  to DI


            services.AddTransient<IValidator<ProvidedServiceCreateDto>, ProvidedServiceCreateDtoValidator>();
            services.AddTransient<IValidator<ProvidedServiceUpdateDto>, ProvidedServiceUpdateDtoValidator>();
            services.AddTransient<IValidator<AdvertisementUpdateDto>, AdvertisementUpdateDtoValidator>();
            services.AddTransient<IValidator<AdvertisementCreateDto>, AdvertisementCreateDtoValidator>();
            services.AddTransient<IValidator<AppUserCreateDto>, AppUserCreateDtoValidator>();
            services.AddTransient<IValidator<AppUserUpdateDto>, AppUserUpdateDtoValidator>();
            services.AddTransient<IValidator<GenderUpdateDto>, GenderUpdateDtoValidator>();
            services.AddTransient<IValidator<GenderCreateDto>, GenderCreateDtoValidator>();
            
            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();
            services.AddTransient<IValidator<AdvertisementAppUserCreateDto>, AdvertisementAppUserCreateDtoValidator>();
         
            
            #endregion




            #region Services Add  to DI
         
            services.AddScoped<IProvidedServiceService, ProvidedServiceService>();
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IGenderService,GenderService>();
            services.AddScoped<IAdvertisementAppUserService, AdvertisementAppUserService>();

            #endregion




        }
    }
}

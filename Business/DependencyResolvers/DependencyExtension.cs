using AutoMapper;
using Business.Mappers;
using Business.MessageAppServices.ApiAuthenticationService;
using Business.MessageAppServices.MessageService;
using Business.MessageAppServices.UserRoleService;
using Business.MessageAppServices.UserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyMessageApp.Core.CacheServices;
using MyMessageApp.Data.Domain.EFDbContext;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork;
using MyMessageApp.Data.MessageRepository.EFCoreRepositories;
using MyMessageApp.Data.UserRepository.EFCoreRepositories;
using MyMessageApp.Data.UserRoleRepository.EFCoreRepositories;
using MyMessageApp.Service.MyMessageApp.Service.Cache.UserCacheServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<Message_App2Context>(opt =>
            {
                opt.UseSqlServer("server=DESKTOP-HJUGI84\\SQLEXPRESS;database=Message_App2;integrated security=true;");
            });

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new MessageProfile());
                opt.AddProfile(new UserProfle());
                opt.AddProfile(new UserRoleProfile());
                opt.AddProfile(new AuthenticationProfile());
            });

            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddMemoryCache();
            //services.AddScoped<ICacheService, CacheService>();

            services.AddScoped<IUnitOfWork,UnitOfWork>();   
            services.AddScoped<IMessageService,MessageService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IUserRoleService,UserRoleService>();
            services.AddScoped<IApiAuthenticationService, ApiAuthenticationService>();


            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            //services.AddScoped<ICacheService, CacheService>();
            //services.AddScoped<IUserCacheService, UserCacheService>();
        }
    }
}

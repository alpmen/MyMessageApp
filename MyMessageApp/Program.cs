using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyMessageApp.Core.Authentication.JWT_Token.JwtModels;
using MyMessageApp.Core.CacheServices;
using MyMessageApp.Core.Context;
using MyMessageApp.Core.ServiceStack;
using MyMessageApp.Data.Domain.EFDbContext;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork;
using MyMessageApp.Data.MessageRepository.EFCoreRepositories;
using MyMessageApp.Data.PanelRolePagesRepositories.EFCoreRepositories;
using MyMessageApp.Data.UserRepository.EFCoreRepositories;
using MyMessageApp.Data.UserRoleRepository.EFCoreRepositories;
using MyMessageApp.Middlewares;
using MyMessageApp.Service.Cache.MessageCacheServices;
using MyMessageApp.Service.Cache.UserCacheServices;
using MyMessageApp.Service.Mappers;
using MyMessageApp.Service.MessageAppServices.ApiAuthenticationService;
using MyMessageApp.Service.MessageAppServices.MessageService;
using MyMessageApp.Service.MessageAppServices.UserRoleService;
using MyMessageApp.Service.MessageAppServices.UserService;
using MyMessageApp.Service.PanelRolePageServices;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

SetupAuthentication();
SetupDI();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
SetupMiddlewares();
app.MapControllers();
app.Run();

void SetupMiddlewares()
{
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseMiddleware<MyMessageApp.Middlewares.AuthorizationMiddleware>();
}

void SetupAuthentication()
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "bearer",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                new List<string>()
            }
        });

        options.CustomSchemaIds(type => type.ToString());
    });

    builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["JWTConfiguration:AccessTokenSecretKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
        };

        o.Events = new JwtBearerEvents()
        {
            OnTokenValidated = context =>
            {
                var claims = JwtTokenValidator.GetClaims(context?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer", "")?.Trim());

                if (claims != null)
                {
                    var apiContext = context.HttpContext.RequestServices.GetService<ApiContext>();

                    apiContext.UserId = claims.Where(x => x.Type == "Id")?.FirstOrDefault()?.Value?.ToInt() ?? 0;
                }

                return Task.CompletedTask;
            }
        };
    });

    builder.Services.AddAuthorization();
    builder.Services.AddEndpointsApiExplorer();
}

void SetupDI()
{
    builder.Services.AddDbContext<Message_App2Context>(options => options.UseSqlServer(builder.Configuration.GetValue<string>("MssqlDbSettings:ConnectionString")), ServiceLifetime.Scoped);

    builder.Services.Configure<JwtTokenSettings>(options => builder.Configuration.GetSection("JWTConfiguration").Bind(options));
    builder.Services.AddScoped<ApiContext>();

    var configuration = new MapperConfiguration(opt =>
    {
        opt.AddProfile(new MessageProfile());
        opt.AddProfile(new UserProfle());
        opt.AddProfile(new UserRoleProfile());
        opt.AddProfile(new AuthenticationProfile());
    });

    var mapper = configuration.CreateMapper();
    builder.Services.AddSingleton(mapper);
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient<IMessageService, MessageService>();
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddTransient<IUserRoleService, UserRoleService>();
    builder.Services.AddTransient<IApiAuthenticationService, ApiAuthenticationService>();
    builder.Services.AddTransient<IMessageRepository, MessageRepository>();
    builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
    builder.Services.AddTransient<IPanelRolePageService, PanelRolePageService>();
    builder.Services.AddTransient<IPanelRolePagesRepository, PanelRolePagesRepository>();
    builder.Services.AddTransient<ICacheService, CacheService>();
    builder.Services.AddTransient<IUserCacheService, UserCacheService>();
    builder.Services.AddTransient<IMessageCacheService, MessageCacheService>();
}
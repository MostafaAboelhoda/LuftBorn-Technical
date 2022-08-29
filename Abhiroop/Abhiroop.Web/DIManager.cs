using Abhiroop.Busines.Auth;
using Abhiroop.Busines.Control.Employees;
using Abhiroop.Busines.Employees;
using Abhiroop.Domain.Context;
using Abhiroop.Domain.Entities;
using Abhiroop.Domain.Helper;
using Abhiroop.Repository;
using Abhiroop.Repository.EmployeeRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Abhiroop.Web
{
    public class DIManager
    {
        public DIManager(IServiceCollection services, IConfiguration configuration)
        {
            AddBusinessInjection(services);
            AddDomainInjection(services);
            AddJWTInjection(services, configuration);
            AddIdentityInjection(services);
            AddFeatures(services, configuration);
            AddControllerInjection(services);
        }
        private void AddIdentityInjection(IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
           .AddEntityFrameworkStores<AbhiroopContext>().AddDefaultTokenProviders();
        }
        private void AddJWTInjection(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection("JWT"));
        }
        private void AddBusinessInjection(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
        private void AddDomainInjection(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
        private void AddControllerInjection(IServiceCollection services)
        {
            services.AddScoped<EmployeeBusinessManager>();
            services.AddScoped<AuthBusinessManager>();
            services.AddScoped<EmployeeRepository>();
        }
        private void AddFeatures(IServiceCollection services, IConfiguration configuration)
        {


            #region ConnectionDb 
            services.AddDbContext<AbhiroopContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("Abhiroop")));
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Abhiroop", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                //x.JsonSerializerOptions.IgnoreNullValues = true;
            });

            #endregion
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(o =>
              {
                  o.RequireHttpsMetadata = false;
                  o.SaveToken = false;
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidIssuer = configuration["JWT:Issuer"],
                      ValidAudience = configuration["JWT:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                      ClockSkew = TimeSpan.Zero
                  };
              });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            #region Mapper
            services.AddSingleton(x =>
            {
                IMapper mapper = new MapperConfiguration(opt =>
                {
                    //opt.AddProfile(new ControllerMapper());

                }).CreateMapper();

                return mapper;
            });
            #endregion
        }
    }
}

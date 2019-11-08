using Figgle;
using Find_A_Tutor.Api.Framework;
using Find_A_Tutor.Core.Mappers;
using Find_A_Tutor.Core.Repositories;
using Find_A_Tutor.Core.Services;
using Find_A_Tutor.Core.Settings;
using Find_A_Tutor.Infrastructure.EF;
using Find_A_Tutor.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace Find_A_Tutor.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public const string PathBaseEnviromentVariable = "PATH_BASE";
        public string PathBase => Configuration[PathBaseEnviromentVariable];

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //.NetCore
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(x =>
            {
                x.SerializerSettings.Formatting = Formatting.Indented;
                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Find-A-Tutor Api", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                var fileName = Path.GetFileName(assemblyName + ".xml");
                c.IncludeXmlComments(Path.Combine(basePath, fileName), includeControllerXmlComments: true);
            });

            //Services
            services.AddScoped<IPrivateLessonService, PrivateLessonService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISchoolSubjectService, SchoolSubjectService>();

            //Repository + SQL
            services.Configure<SqlSettings>(Configuration);
            var sqlSettings = Configuration.GetSection("sql").Get<SqlSettings>();
            services.AddSingleton<SqlSettings>(sqlSettings); //todo: Options pattern

            if (sqlSettings.InMemory)
            {
                services.AddScoped<IPrivateLessonRepository, InMemoryPrivateLessonRepository>();
                services.AddScoped<IUserRepository, InMemoryUserRepository>();
                services.AddScoped<ISchoolSubjectRepository, InMemorySchoolSubjectRepository>();
            }
            else
            {
                services.AddScoped<IPrivateLessonRepository, PrivateLessonRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<ISchoolSubjectRepository, SchoolSubjectRepository>();
            }

            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<FindATurorContext>(options => options.UseSqlServer(sqlSettings.ConnectionString));

            //Healthchecks
            services.AddHealthChecks()
                    .AddSqlServer(Configuration["sql:connectionString"])
                    .AddDbContextCheck<FindATurorContext>("FindATurorContext-HealthCheck");

            //Automapper
            services.AddSingleton(AutoMapperConfig.Initialize());

            //Authorization
            services.AddAuthorization(x => x.AddPolicy("HasAdminRole", p => p.RequireRole("admin")));
            services.AddAuthorization(x => x.AddPolicy("HasTutorRole", p => p.RequireRole("tutor")));
            services.AddAuthorization(x => x.AddPolicy("HasStudentRole", p => p.RequireRole("student")));

            //JWT
            services.AddSingleton<IJwtHandler, JwtHandler>();
            var jwtSettings = Configuration.GetSection("jwt");

            services.Configure<JwtSettings>(jwtSettings);
            services.AddAuthentication().AddJwtBearer(o =>
            {
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.GetValue<string>("issuer"),
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("key")))
                };
            });

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Console.WriteLine(FiggleFonts.Standard.Render("Find-A-Tutor API"));
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            app.UseSwagger()
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        $"{ (!string.IsNullOrEmpty(PathBase) ? PathBase : string.Empty)}/swagger/v1/swagger.json",
                        "Find-A-Tutor Api");
                });

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonConvert.SerializeObject(
                        new
                        {
                            status = report.Status.ToString(),
                            details = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                        });
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseErrorHandler();

            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

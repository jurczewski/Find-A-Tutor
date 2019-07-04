using Find_A_Tutor.Core.Repositories;
using Find_A_Tutor.Infrastructure.Mappers;
using Find_A_Tutor.Infrastructure.Repositories;
using Find_A_Tutor.Infrastructure.Services;
using Find_A_Tutor.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Find_A_Tutor.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);

            services.AddAuthorization(x => x.AddPolicy("HasAdminRole", p => p.RequireRole("admin")));
            services.AddAuthorization(x => x.AddPolicy("HasTutorRole", p => p.RequireRole("tutor")));
            services.AddAuthorization(x => x.AddPolicy("HasStudentRole", p => p.RequireRole("student")));

            services.AddScoped<IDataInitializer, DataInitializer>();
            services.AddScoped<IPrivateLessonRepository, PrivateLessonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPrivateLessonService, PrivateLessonService>();
            services.AddScoped<IUserService, UserService>();


            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddSingleton<IJwtHandler, JwtHandler>();

            var appSettings = Configuration.GetSection("app");
            services.Configure<AppSettings>(appSettings);

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            SeedData(app);
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
        private void SeedData(IApplicationBuilder app)
        {
            var settings = app.ApplicationServices.GetService<IOptions<AppSettings>>();
            if (settings.Value.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }
        }
    }
}

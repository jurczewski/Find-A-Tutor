using Figgle;
using Find_A_Tutor.Frontend.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Find_A_Tutor.Frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine(FiggleFonts.Standard.Render("Find-A-Tutor Frontend"));

            services.AddSession();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var apiUrl = Configuration.GetSection("ApiUrl");
            services.AddHealthChecks()
                    .AddUrlGroup(new Uri($"{apiUrl?.Value}/ping"), "Find-A-Tutor.Api");

            ApiHelper.InitializeClient();
            services.AddScoped<IPrivateLessonService, PrivateLessonService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISchoolSubjectService, SchoolSubjectService>();
            services.AddScoped<IPaymentService, PaymentService>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //HealthChecks
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc();
        }
    }
}

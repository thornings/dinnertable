using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientMadbordet.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ClientMadbordet.Models;

namespace ClientMadbordet
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddDbContext<CalendarContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MadbordetDatabase")));

            services.AddIdentity<AppUser, AppRole>()
               .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Calendar/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}",
                    defaults: new { controller = "Calendar", action = "Index" });

                routes.MapRoute(
                    name: "food",
                    template: "{controller=Food}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "meal",
                   template: "{controller=Meal}/{action=Index}");

                routes.MapRoute(
                 name: "calendarfoodCreate",
                 template: "{controller=CalendarFood}/{action=Create}/{year}/{month}/{day}/{foodId}/{mealId}");

                routes.MapRoute(
                 name: "calendarfoodAdd",
                 template: "{controller=CalendarFood}/{action=Add}/{year?}/{month?}/{day?}/{mealId?}");

                routes.MapRoute(
                 name: "calendarfoodDelete",
                 template: "{controller=CalendarFood}/{action=Delete}/{back}/{foodItemId}");

                routes.MapRoute(
                    name: "Calendar",
                    template: "{controller=Calendar}/{action=Index}/{year:int}/{month:int}/{day:int}"
                );

        });
        }
    }
}

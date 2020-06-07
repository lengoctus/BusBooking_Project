using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.CsRepository;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusBooking_Project
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
            services.AddControllersWithViews();
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IBusRePo, BusRepo>();
            services.AddScoped<ISeatRePo, SeatRepo>();
            services.AddDbContext<ConnectDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectDb")));
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "SCHEME_AD";
            })
            .AddCookie("SCHEME_AD", option =>
            {
                option.LoginPath = "/admin/login";
                option.AccessDeniedPath = "/admin/accessDenied";
                option.LogoutPath = "/admin/logout";
                option.Cookie.Name = "acecookie";
            });
            //.AddCookie("SCHEME_EMP", option =>
            //{
            //    option.LoginPath = "";
            //    option.AccessDeniedPath = "";
            //    option.LogoutPath = "";
            //    option.Cookie.Name = "";
            //});
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.Use(async (context, next) =>
            {
                ClaimsPrincipal principal = new ClaimsPrincipal();
                var result = await context.AuthenticateAsync("SCHEME_AD");
                if (result?.Principal != null)
                {
                    principal.AddIdentities(result.Principal.Identities);
                }
                context.User = principal;
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //Của sáng
                endpoints.MapControllerRoute(
                    name: "admin_route",
                    pattern: "{area:exists}/{controller}/{action}/{id?}",
                    defaults: new { area = "admin" },
                    constraints: new { area = "admin" });
                //Của sáng//


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapAreaControllerRoute(
                    name: "Employee",
                    areaName: "Employee",
                    pattern: "Employee/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

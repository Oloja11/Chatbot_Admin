using ChatbotAdmin.filter;
using ChatbotAdmin.Models;
using ChatbotAdmin.Repository;
using ChatbotAdmin.Repository.Implementation;
//using ChatbotAdmin.Repository.Implementation;
using ChatbotAdmin.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubEnrollment
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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                
            });
            
            //services.AddTransient<DbLogger<T>,
            services.AddTransient<IStartupFilter, DatabaseInitFilter>();
            services.Configure<ApplicationConfig>(Configuration.GetSection("ApplicationConfig"));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            
            services.AddControllersWithViews().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddSingleton<IUserManagementRepository, UserManagementRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddTransient<ILoginManager, LoginManager>();
            services.AddTransient<UserManagementService>();
            services.AddTransient<MessageService>();
            services.AddSingleton<UtilService>();
            services.AddSingleton<EmailSenderService>();
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
            app.UseCookiePolicy();
            

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

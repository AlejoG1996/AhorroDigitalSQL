using AhorroDigital.API.Data;
using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vereyon.Web;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Azure;
using Azure.Storage.Queues;
using Azure.Storage.Blobs;
using Azure.Core.Extensions;

namespace AhorroDigital.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddIdentity<User, IdentityRole>(x =>
            {
                x.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                x.SignIn.RequireConfirmedEmail = true;
                x.User.RequireUniqueEmail = true;
                x.Password.RequireDigit = false;
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireUppercase = false;


            }).AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DataContext>();



            services.AddDbContext<DataContext>(x =>
           {
               x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
           });
            services.AddTransient<SeedDb>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<ICombosHelper, CombosHelper>();
            services.AddScoped<IConverterHelper, ConverterHelper>();
            services.AddScoped<IBlobHelper, BlobHelper>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddFlashMessage();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
               
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                     pattern: "{controller=Home}/{action=IndexHome}/{id?}");
            });
        }
    }

}

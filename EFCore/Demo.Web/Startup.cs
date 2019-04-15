using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Demo.Web.Service;
using EFCore.Data;
using EFCore.DomainModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Demo.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<MyContext>(options =>
                {
                    options.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
                });
            services.AddSingleton<IWelcomeService, WelcomeService>();
            services.AddScoped<IRepository<Province>, InMemoryRepository>();

            //添加数据库Identity相关表
            services.AddDbContext<IdentityDbContext>(options =>
                {
                    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("Demo.Web"));
                });
            
            //注入服务
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 10;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IConfiguration configuration,
            IWelcomeService welcomeService,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(next =>
            {
                logger.LogInformation("app.use....");
                return async httpContext =>
                {
                    logger.LogInformation("-----async httpcontext...");
                    if (httpContext.Request.Path.StartsWithSegments("/first"))
                    {
                        logger.LogInformation(".....first....");
                        await httpContext.Response.WriteAsync("first!!!");
                    }
                    else
                    {
                        logger.LogInformation(".....next(httpContext)...");
                        await next(httpContext);
                    }
                };
            });

            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/Welcome"
            });

            // //配置默认页，否则默认index.html
            // app.UseDefaultFiles("/default.html");
            // app.UseStaticFiles();

            //app.UseFileServer();

            app.UseStaticFiles();

            //允许bootstrap访问目录中间件
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules"))
            });

            //身份验证中间件
            app.UseAuthentication();

            app.UseMvc(builder => { builder.MapRoute("Default", "{controller=home}/{action=index}/{id?}"); });

            app.Run(async (context) =>
            {
                var welcome = welcomeService.GetMessage();
                //var welcome = configuration["Welcome"];
                await context.Response.WriteAsync(welcome);
            });
        }
    }
}

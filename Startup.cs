using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProCoding.Demos.ASPNETCore.Walkthrough.Models;

namespace ProCoding.Demos.ASPNETCore.Walkthrough
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

            services.AddDbContext<TarefasDbContext>(options => 
                options.UseSqlite("Data Source=tarefas.db")
                // options.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDb;Initial Catalog=Tarefas;Integrated Security=SSPI;AttachDBFilename=Tarefas.mdf")
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

           app.ApplicationServices.CreateScope()
               .ServiceProvider.GetService<TarefasDbContext>()
               .Database.EnsureCreated();

            app.UseMvc(routes =>
            {
                

                // routes.MapRoute(
                //     name: "tasksRoute",
                //     template: "tasks/{taskId?}",
                //     defaults: new {
                //         Controller = "Tarefas",
                //         Action = "Detalhes"
                //     });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

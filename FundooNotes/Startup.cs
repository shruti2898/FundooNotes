using FundooRepository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooManager.Interface;
using FundooManager.Manager;
using FundooRepository.Repository;
using FundooRepository.Interface;

namespace FundooNotes
{
    public class Startup
    {
        public IConfiguration configuration { get;}
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContextPool<UserContext>(options => options.UseSqlServer(this.configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IUserRepository,UserRepository>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<INotesRepository, NotesRepository>();
            services.AddTransient<INotesManager, NotesManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

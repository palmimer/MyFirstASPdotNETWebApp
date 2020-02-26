using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstWebApp.Models;
using MyFirstWebApp.Services;

namespace MyFirstWebApp
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
            services.AddRazorPages();
            //ide meg beírjuk a kontrollereket
            services.AddControllers();
            //ide beírjuk a saját megírt service-ünket (és using MyFirstWebApp.Services)
            services.AddTransient<JsonFilePersonService>();
            
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //endpoints are where a url turns into some work
            app.UseEndpoints(endpoints =>
            {
            endpoints.MapRazorPages();
                //adding a new endpoint
                endpoints.MapControllers();
            //endpoints.MapGet("/users", (context) => 
            //        {
            //            //we are collecting all the person objects in a collection with the help of our service
            //            var persons = app.ApplicationServices.GetService<JsonFilePersonService>().GetPersons();
            //            //we format the data into json
            //            var json = JsonSerializer.Serialize<IEnumerable<Person>>(persons);
            //            //we return the data to the web page
            //            return context.Response.WriteAsync(json);
            //        });
            });
        }
    }
}

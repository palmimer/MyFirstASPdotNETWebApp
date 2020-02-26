using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyFirstWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // egy host-ot hozunk létre
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //itt kéri meg, hogy használja a Startup class-t (ami ASP specifikus, előre megírt kód)
                    webBuilder.UseStartup<Startup>();
                });
    }
}

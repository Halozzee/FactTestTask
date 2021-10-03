using DataLayer;
using DataLayer.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FactTestTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var assemPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var root = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(assemPath).FullName).FullName).FullName).FullName;
            var host = CreateHostBuilder(args).Build();
            if (DrinkDeliveryMaster.IsSampleDataNeeded())
                SampleData.InitData();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

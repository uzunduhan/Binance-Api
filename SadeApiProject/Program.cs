using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SadeApiProject
{
    public class Program
    {
       
       
        public static void Main(string[] args)
        {
            Controllers.BnbController.insertBnbBtcValueToDb();
            Controllers.BchController.insertBchBtcValueToDb();
            Controllers.EthController.insertEthBtcValueToDb();
            Controllers.LtcController.insertLtcBtcValueToDb();
            Controllers.XrpController.insertXrpBtcValueToDb();

            CreateHostBuilder(args).Build().Run();
           
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

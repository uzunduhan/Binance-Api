using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.NetCore;

namespace SadeApiProject.Controllers
{
    public class ApiConnection
    {
        static string apiKey = "xLA3H8uo8SRdVue4mbh1NGpQnSRJu8UNjp4IigWLVkfSJsKQYy7hXnxlK1vCKywc";
        static string secretKey = "muYmt9yUM6fuFRoxruC5BQHlXejfBY4rprlRb9i0seERPwpdU9zEsxdBtbk4gF4L";

        public static BinanceApiClient returnApiClient()
        {
            var apiClient = new BinanceApiClient(apiKey, secretKey);

            return apiClient;
        }

    }
}

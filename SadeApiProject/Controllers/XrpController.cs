using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.NetCore;
using System.Text.Json;
using System.Threading;
using System.Globalization;

namespace SadeApiProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class XrpController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {

            PostgreCon con = new PostgreCon();

            decimal saatlikOrtalama = 0;
            decimal son = 0;
            decimal sonIki = 0;
            decimal sonUc = 0;


            List<decimal> datas = new List<decimal>();

            datas =  con.getData("xrpbtc");

            

            if (datas.Count() >= 12)
            { 
                for(int i = 0; i < datas.Count(); i++)
                {
                    saatlikOrtalama += datas.ElementAt(i);
                }

                saatlikOrtalama = saatlikOrtalama / 12;
            }

            if (datas.Count() >= 3)
            {
                son = datas.ElementAt(0);
                sonIki = datas.ElementAt(1);
                sonUc = datas.ElementAt(2);
            }


            var XrpBtc = new Crypto
            {
                sonDeger = son.ToString(),
                sondanOncekiDeger = sonIki.ToString(),
                sondanOncekiIkinciDeger = sonUc.ToString(),
                saatlikOrtalama = saatlikOrtalama.ToString()
            };

            string jsonString = JsonSerializer.Serialize(XrpBtc);

            datas.Clear();
            return jsonString;



        }

        static string getXrpBtcValue()
        {

            BinanceApiClient client = new BinanceApiClient();

            return client.GetTicker("XRPBTC").Price.ToString();

        }


        public static void insertXrpBtcValueToDb()
        {
            PostgreCon con = new PostgreCon();

            Task.Run(() =>
            {
                while (true)
                {
                    con.insertData(getXrpBtcValue(), "xrpbtc");

                    //Console.WriteLine("xrp/btc eklendi");
                    Thread.Sleep(30000);
                }

            });


        }
    }
}

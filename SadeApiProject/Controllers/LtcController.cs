using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.NetCore;
using System.Text.Json;
using System.Threading;

namespace SadeApiProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LtcController : ControllerBase
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

            datas = con.getData("ltcbtc");


            if (datas.Count() >= 12)
            {
                for (int i = 0; i < datas.Count(); i++)
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



            var LtcBtc = new Crypto
            {
                sonDeger = son.ToString(),
                sondanOncekiDeger = sonIki.ToString(),
                sondanOncekiIkinciDeger = sonUc.ToString(),
                saatlikOrtalama = saatlikOrtalama.ToString()
            };

            string jsonString = JsonSerializer.Serialize(LtcBtc);

            datas.Clear();
            return jsonString;



        }

        static string getLtcBtcValue()
        {

            BinanceApiClient client = new BinanceApiClient();

            return client.GetTicker("LTCBTC").Price.ToString();

        }


        public static void insertLtcBtcValueToDb()
        {
            PostgreCon con = new PostgreCon();

            Task.Run(() =>
            {
                while (true)
                {
                    con.insertData(getLtcBtcValue(), "ltcbtc");

                    //Console.WriteLine("ltc/btc eklendi");
                    Thread.Sleep(30000);
                }

            });


        }
    }
}

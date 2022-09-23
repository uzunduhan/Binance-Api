using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.NetCore;
using System.Threading;
using System.Text.Json;

namespace SadeApiProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EthController : ControllerBase
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

            datas = con.getData("ethbtc");


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



            var EthBtc = new Crypto
            {
                sonDeger = son.ToString(),
                sondanOncekiDeger = sonIki.ToString(),
                sondanOncekiIkinciDeger = sonUc.ToString(),
                saatlikOrtalama = saatlikOrtalama.ToString()
            };

            string jsonString = JsonSerializer.Serialize(EthBtc);

            datas.Clear();
            return jsonString;



        }



        static string getEthBtcValue()
        {

            BinanceApiClient client = new BinanceApiClient();

            return client.GetTicker("ETHBTC").Price.ToString();

        }


        public static void insertEthBtcValueToDb()
        {
            PostgreCon con = new PostgreCon();

            Task.Run(() =>
            {
                while (true)
                {
                    con.insertData(getEthBtcValue(), "ethbtc");

                    //Console.WriteLine("eth/btc eklendi");
                    Thread.Sleep(30000);
                }

            });


        }


    }
}

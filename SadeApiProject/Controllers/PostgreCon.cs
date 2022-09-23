using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using System.Threading;

namespace SadeApiProject.Controllers
{

    class PostgreCon
    {
        List<double> datas = new List<double>();
        List<decimal> data = new List<decimal>();

        NpgsqlConnection connection = new NpgsqlConnection("server=localhost; port=5432;" +
            "Database=sade; user ID=postgres; password=123456");

        public void insertData(string val, string where)
        {
            string query = "";

            connection.Open();

            if (where == "bnbbtc")
            {
                query = @"insert into public.bnbbtc(value)values(@val)";
            }

            else if (where == "ethbtc")
            {
                query = @"insert into public.ethbtc(value)values(@val)";
            }

            else if (where == "xrpbtc")
            {
                query = @"insert into public.xrpbtc(value)values(@val)";
            }

            else if (where == "bchbtc")
            {
                query = @"insert into public.bchbtc(value)values(@val)";
            }

            else if (where == "ltcbtc")
            {
                query = @"insert into public.ltcbtc(value)values(@val)";
            }

            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@val", val);
            cmd.ExecuteNonQuery();
            connection.Close();
        }


        public List<decimal> getData(string type)
        {
            data.Clear();

            string query = "";


            connection.Open();

            if (type == "ethbtc")
            {
                query = @"select * from public.ethbtc order by id desc limit 12";
            }

            else if (type == "bchbtc")
            {
                query = @"select * from public.bchbtc order by id desc limit 12";
            }

            else if (type == "xrpbtc")
            {
                query = @"select * from public.xrpbtc order by id desc limit 12";
            }

            else if (type == "bnbbtc")
            {
                query = @"select * from public.bnbbtc order by id desc limit 12";
            }

            else if (type == "ltcbtc")
            {
                query = @"select * from public.ltcbtc order by id desc limit 12";
            }


            var cmd = new NpgsqlCommand(query, connection);

            NpgsqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                data.Add(Convert.ToDecimal(rdr.GetString(1)));
            }

            connection.Close();

            return data;

        }
    }
}

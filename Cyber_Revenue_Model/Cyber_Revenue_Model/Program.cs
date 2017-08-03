using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cyber_Revenue_Model
{
    class Program
    {
        static void Main(string[] args)
        {

            string URL = "https://api.intrinio.com/financials/reported"+
                "?identifier=GOOGL"+
                "&statement=income_statement"+
                "&&fiscal_year=2016"+
                "&fiscal_period=Q3" +
                "&API_USERNAME:API_PASSWORD";
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.ContentType = "application/json; charset=utf-8";
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("701838466ee4639736a4db8e8dd4f32c:ee5d16f3bc0d4b3f3f7cea2f4069699e"));
            request.PreAuthenticate = true;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            var jsonres = response.GetResponseStream().ToString();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                // Console.WriteLine(reader.ReadToEnd());
                /*Console.WriteLine(reader.ReadToEnd());
                Console.ReadKey();
                */
                Financial_Data data = JsonConvert.DeserializeObject<Financial_Data>(reader.ReadToEnd());
               // var revenue = data.data;
                Console.WriteLine(data.data[0].value);
                Console.ReadKey();
            }
        }
    }

    public class Financial_Data

    {
        public Datum[] data { get; set; }
        public int result_count { get; set; }
        public int page_size { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public int api_call_credits { get; set; }
    }

    public class Datum
    {
        public string xbrl_tag { get; set; }
        public string domain_tag { get; set; }
        public string value { get; set; }
    }

}




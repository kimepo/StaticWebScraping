using StaticWebScraping;
using System.Net.Http.Json;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CsvHelper;
using System.IO;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace WebScraping
{
    class program
    {
       static void Main(string[] args)
        {
            var corUrl = "https://clientportal.jse.co.za/reports/silo-unavailability-report";
            string json = string.Empty;
            HttpWebRequest webRequest;
            webRequest = (HttpWebRequest)WebRequest.Create(corUrl);

            webRequest.Timeout = 300000;
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36 Edg/129.0.0.0";

            json = string.Empty;
            using (var response = webRequest.GetResponse())
            {
                var result = ((HttpWebResponse)response).StatusCode;
                using (var responseStream = response.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        json = streamReader.ReadToEnd();
                    }
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var result = JsonConvert.DeserializeObject<Trades>(json);
                    Console.WriteLine(string.Join("\t", result));
                    foreach (var row in result.Property1)
                    {
                        Console.WriteLine(string.Join("\t", row));
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
//var writer = new StreamWriter("infoTrade.csv" + DateTime.Today);
//var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
//{
//    csv.WriteRecords(Trades);
//}





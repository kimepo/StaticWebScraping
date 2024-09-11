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

namespace WebScraping
{
    class program
    {
       static void Main(string[] args)
        {
            try
            {
                var resultat = GetData(url: "https://clientportal.jse.co.za/reports/silo-unavailability-report");
                if (resultat != null) 
                {
                    Console.WriteLine(string.Join("\t", resultat));
                    foreach (var row in resultat.Property1) 
                    {
                        Console.WriteLine(string.Join("\t",row));
                    }
                }
                Console.ReadKey();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }

            //var writer = new StreamWriter("infoTrade.csv" + DateTime.Today);
            //var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            //{
            //    csv.WriteRecords(Trades);
            //}

        }

        private static Trades GetData(string url)
        {
            try 
            {
                using (HttpClientHandler hdl = new HttpClientHandler 
                {
                    AllowAutoRedirect = false,
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.None,
                    CookieContainer = new System.Net.CookieContainer() 
                }) 
                {
                    using (HttpClient clnt = new HttpClient(hdl, false)) 
                    {
                        clnt.DefaultRequestHeaders.Add("User-Agent:", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36 Edg/128.0.0.0");
                        clnt.DefaultRequestHeaders.Add("Accept:", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                        clnt.DefaultRequestHeaders.Add("Accept-Language:", "ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7");
                        //clnt.DefaultRequestHeaders.Add("Connection", "");
                        clnt.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests:", "1");
                        //clnt.DefaultRequestHeaders.Add("Accept-Encoding:", "gzip, deflate, br, zstd");


                        using (var resp = clnt.GetAsync(url).Result) 
                        {
                            if (!resp.IsSuccessStatusCode) 
                            {
                                return null;
                            }
                        }
                    }

                    using (HttpClient clnt = new HttpClient(hdl, false))
                    {
                        clnt.DefaultRequestHeaders.Add("User-Agent:", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36 Edg/128.0.0.0");
                        clnt.DefaultRequestHeaders.Add("Accept:", "application/json, text/javascript, */*; q=0.01");
                        clnt.DefaultRequestHeaders.Add("Accept-Language:", "ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7");

                        clnt.DefaultRequestHeaders.Add("X-Requested-With::", "XMLHttpRequest");
                        clnt.DefaultRequestHeaders.Add("Referer:", url);

                        using (var resp = clnt.GetAsync("https://clientportal.jse.co.za/_vti_bin/JSE/CommoditiesService.svc/GetSiloData").Result)
                        {
                            var json = resp.Content.ReadAsStringAsync().Result;
                            if(!string.IsNullOrEmpty(json))
                            {
                                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Trades>(json);
                                return result;
                            }
                        }

                    }

                }
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            return null;
        }
    }

}

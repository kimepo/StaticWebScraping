using StaticWebScraping;

using CsvHelper;
using CsvHelper.Configuration;

using System.Globalization;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Formats.Asn1;
using System.Xml.Linq;
using System.Data;

namespace WebScraping
{
    class program
    {
       static void Main()
        {
            List<Class1> Stock = new List<Class1>();

            

            var corUrl = "https://clientportal.jse.co.za/_vti_bin/JSE/CommoditiesService.svc/GetSiloData";
            string json = string.Empty;
            HttpWebRequest webRequest;
            webRequest = (HttpWebRequest)WebRequest.Create(corUrl);

            webRequest.Timeout = 300000;
            webRequest.ContentType = "application/json;";
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
                    


                    JArray resultstkObjects = (JArray)(JToken.Parse(json));
                    foreach (JObject result in resultstkObjects)
                    {
                        Class1 stk = new Class1();
                        foreach (JProperty property in result.Properties())
                        {
                            switch (property.Name)
                            {
                                case "Comment":
                                    stk.Comment = property.Value.ToString() +";";
                                    Console.WriteLine(stk.Comment);
                                    break;

                                case "FromDate":
                                    stk.FromDate = property.Value.ToString() + ";";
                                    Console.WriteLine(stk.FromDate);
                                    break;

                                case "ID":
                                    stk.ID = property.Value.ToString() + ";";
                                    Console.WriteLine(stk.ID);
                                    break;

                                case "Location":
                                    stk.Location = property.Value.ToString() + ";";
                                    Console.WriteLine(stk.Location);
                                    break;

                                case "Operator":
                                    stk.Operator = property.Value.ToString() + ";";
                                    Console.WriteLine(stk.Operator);
                                    break;

                                case "Reason":
                                    stk.Reason = property.Value.ToString() + ";";
                                    Console.WriteLine(stk.Reason);
                                    break;

                                case "Region":
                                    stk.Region = property.Value.ToString() + ";";
                                    Console.WriteLine(stk.Region);
                                    break;

                                case "ToDate":
                                    stk.ToDate = property.Value.ToString() + ";";
                                    Console.WriteLine(stk.ToDate);
                                    break;

                                    
                            }
                            



                        }
                        Stock.Add(stk);
                    }
                    Console.WriteLine("нажмите любую клавишу для создания csv");
                    //Console.ReadKey();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            try
            {
                DateTime now = DateTime.Now;
                string name = $"infoTrade.csv_{now.Year}_{now.Month}_{now.Day}.csv";
                using var writer = new StreamWriter(name);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                {

                    

                    csv.WriteHeader<Class1>();
                    csv.NextRecord();
                    

                    foreach (var stock in Stock)
                    {
                        csv.WriteRecord(stock );
                        csv.NextRecord();
                    }
                    csv.Dispose();
                }
                
                Console.WriteLine("файл csv создан");

            }
            catch (Exception ex) { Console.WriteLine("неудалось перевести в формат csv,код ошибки:" + (ex.Message)); }


        }
    }
}

//+DateTime.Today




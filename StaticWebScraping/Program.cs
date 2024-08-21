using HtmlAgilityPack;
using StaticWebScraping;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CsvHelper;
using System.IO;
using System.Text;
using System.Globalization;

namespace WebScraping
{
    public class program
    {
        public static void Main()
        {
            string url =
                "https://clientportal.jse.co.za/reports/delta-option-and-structured-option-trades";

            var web = new HtmlWeb();
            // downloading to the target page
            // and parsing its HTML content
            var document = web.Load(url);
            var nodes = document.DocumentNode.SelectNodes("//*[@id='tableTrades']");
            List<Trades> Trades = new List<Trades>();
            foreach ( var node in nodes )
            {
                Trades.Add(new Trades()
                {
                    TradeDate = HtmlEntity.DeEntitize(node.SelectSingleNode("th[1]").InnerText),

                    TradeType = HtmlEntity.DeEntitize(node.SelectSingleNode("td[2]").InnerText),

                    ShortName = HtmlEntity.DeEntitize(node.SelectSingleNode("td[3]").InnerText),

                    FutureExpiry = HtmlEntity.DeEntitize(node.SelectSingleNode("td[4]").InnerText),

                    Strike = HtmlEntity.DeEntitize(node.SelectSingleNode("td[5]").InnerText),

                    CallPut = HtmlEntity.DeEntitize(node.SelectSingleNode("td[6]").InnerText),

                    Quantity = HtmlEntity.DeEntitize(node.SelectSingleNode("td[7]").InnerText),

                    Vol = HtmlEntity.DeEntitize(node.SelectSingleNode("td[8]").InnerText),

                    Premium = HtmlEntity.DeEntitize(node.SelectSingleNode("td[9]").InnerText),

                    FuturesPrice = HtmlEntity.DeEntitize(node.SelectSingleNode("td[10]").InnerText)

                });
            }

            using (var writer = new StreamWriter("infoTrade.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Trades);
            }

        }
    }
}

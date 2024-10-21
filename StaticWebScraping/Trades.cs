using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using OpenQA.Selenium.DevTools.V125.CSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebScraping
{
    //[JsonArrayAttribute]
    public class Trades
    {
        public Class1[] Property1 { get; set; }

    }
    
    public class Class1
        {

        [Name("Comment")]
        public string? Comment { get; set; }

        [Name("FromDate")]
        public string? FromDate { get; set; }

        [Name("ID")]
        public string? ID { get; set; }

        [Name("Location")]
        public string? Location { get; set; }

        [Name("Operator")]
        public string? Operator { get; set; }

        [Name("Reason")]
        public string? Reason { get; set; }

        [Name("Region")]
        public string? Region { get; set; }

        [Name("ToDate")]
        public string? ToDate { get; set; }

       
    }

}


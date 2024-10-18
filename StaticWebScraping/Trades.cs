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

        [JsonProperty(PropertyName = "Comment")]
        public string? Comment { get; set; }

        [JsonProperty(PropertyName = "FromDate")]
        public string? FromDate { get; set; }

        [JsonProperty(PropertyName = "ID")]
        public string? ID { get; set; }

        [JsonProperty(PropertyName = "Location")]
        public string? Location { get; set; }

        [JsonProperty(PropertyName = "Operator")]
        public string? Operator { get; set; }

        //[JsonProperty(PropertyName = "Reason")]
        public string? Reason { get; set; }

        //[JsonProperty(PropertyName = "Region")]
        public string? Region { get; set; }

        //[JsonProperty(PropertyName = "ToDate")]
        public string? ToDate { get; set; }

       
    }

}


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipyBotWeb.Service
{
    public class WebApiConnectorService
    {
        public static T GenericGetRequest<T>(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            string jsonResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonResponse = reader.ReadToEnd();
            }
            var rateData = JsonConvert.DeserializeObject<T>(jsonResponse);
            return rateData;
        }
    }
}
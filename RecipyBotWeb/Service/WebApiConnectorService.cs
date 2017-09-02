using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace RecipyBotWeb.Service
{
    public class WebApiConnectorService
    {
        public static T GenericGetRequest<T>(string url)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.AutomaticDecompression = DecompressionMethods.GZip;
            //string jsonResponse = string.Empty;
            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //using (Stream stream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    jsonResponse = reader.ReadToEnd();
            //}
            string result = string.Empty;
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                //client.BaseAddress = new Uri(url);
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }

            var rateData = JsonConvert.DeserializeObject<T>(result);
            return rateData;
        }
    }
}
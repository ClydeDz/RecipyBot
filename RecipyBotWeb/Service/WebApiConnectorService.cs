using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace RecipyBotWeb.Service
{
    public class WebApiConnectorService
    {
        public static T GenericGetRequest<T>(string url)
        {
            string result = string.Empty;
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }

            var rateData = JsonConvert.DeserializeObject<T>(result);
            return rateData;
        }
    }
}
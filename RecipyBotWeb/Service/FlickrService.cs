using RecipyBotWeb.Constants;
using RecipyBotWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipyBotWeb.Service
{
    public class FlickrService
    {
        public static string GetImage(string query)
        {
            string apiCall = BotConstants.BotApiSettings.FlickrImageApi + "&method=flickr.photos.search&text=" + HttpContext.Current.Server.UrlEncode(query) + "&format=json&nojsoncallback=1";
            FlickrDataModel webResponse = WebApiConnectorService.GenericGetRequest<FlickrDataModel>(apiCall); 
            IEnumerable <int> randomNumbers = MiscService.GiveXFromYNumbers(1, webResponse.photos.photo.Count);
            string _imageUrl = string.Empty;
            foreach (var value in randomNumbers)
            {
                _imageUrl = "https://farm"+ webResponse.photos.photo.ElementAt(value).farm + ".staticflickr.com/" + webResponse.photos.photo.ElementAt(value).server + "/" + webResponse.photos.photo.ElementAt(value).id + "_" + webResponse.photos.photo.ElementAt(value).secret + ".jpg";
            }
            return _imageUrl;
        }
    }
}
using RecipyBotWeb.Constants;
using RecipyBotWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RecipyBotWeb.Service
{
    public class FlickrService
    {
        /// <summary>
        /// Gets an image from the Flickr API for the query supplied.
        /// Returns a default RecipyBot image if none found from Flickr.
        /// </summary>
        public static string GetImage(string query)
        {
            try
            {
                string apiCall = BotConstants.BotApiSettings.FlickrImageApi + "&method=flickr.photos.search&text=" + HttpContext.Current.Server.UrlEncode(query) + "&format=json&nojsoncallback=1";
                FlickrDataModel webResponse = WebApiConnectorService.GenericGetRequest<FlickrDataModel>(apiCall);
                IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(1, webResponse.photos.photo.Count);
                string _imageUrl = string.Empty;
                foreach (var value in randomNumbers)
                {
                    _imageUrl = "https://farm" + webResponse.photos.photo.ElementAt(value).farm + ".staticflickr.com/" + webResponse.photos.photo.ElementAt(value).server + "/" + webResponse.photos.photo.ElementAt(value).id + "_" + webResponse.photos.photo.ElementAt(value).secret + ".jpg";
                }
                return _imageUrl;
            }
            catch (NullReferenceException nex)
            {
                Debug.WriteLine("Flickr GetImage " + nex.Message);
                return "https://raw.githubusercontent.com/ClydeDz/RecipyBot/master/Docs/recipybot/2.png";
            }
            catch (Exception e)
            {
                Debug.WriteLine("Flickr GetImage " + e.Message);
                return "https://raw.githubusercontent.com/ClydeDz/RecipyBot/master/Docs/recipybot/2.png";
            }
        }
    }
}
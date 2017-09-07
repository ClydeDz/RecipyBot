using RecipyBotWeb.Constants;
using RecipyBotWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipyBotWeb.Service
{
    public class QwantService
    {
        public static string GetImage(string query)
        {
            QwantDataModel webResponse = WebApiConnectorService.GenericGetRequest<QwantDataModel>(BotConstants.BotApiSettings.QwantImageApi + "" + query);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(1, webResponse.data.result.items.Count());
            string _imageUrl = string.Empty;
            foreach (var value in randomNumbers)
            {
                var recipe = webResponse.data.result.items.ElementAt(value);
                _imageUrl = recipe.media.ToString();
            }
            return _imageUrl;
        }
    }
}
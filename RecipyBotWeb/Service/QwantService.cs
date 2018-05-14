using RecipyBotWeb.Constants;
using RecipyBotWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RecipyBotWeb.Service
{
    public class QwantService
    {
        /// <summary>
        /// Gets an image from the Qwant API for the query supplied.
        /// Returns a default RecipyBot image if none found from Qwant.
        /// </summary>
        public static string GetImage(string query)
        {
            try
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
            catch (NullReferenceException nex)
            {
                System.Diagnostics.Trace.TraceError("QwantService GetImage " + nex.Message);
                return "https://github.com/ClydeDz/RecipyBot/blob/master/Docs/recipybot/3.png";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("QwantService GetImage " + e.Message);
                return "https://github.com/ClydeDz/RecipyBot/blob/master/Docs/recipybot/3.png";
            }           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipyBotWeb.Models;
using RecipyBotWeb.Constants;

namespace RecipyBotWeb.Service
{
    public class RecipePuppyService
    {
        public static void GetAllRecipies()
        {
            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(ApiUrls.RecipePuppyGet);
        }
    }
}
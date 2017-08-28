using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Configuration;

namespace RecipyBotWeb.Constants
{
    public class SiteSettingsConstant
    {
        public static string RecipePuppyWebApiUrl
        {
            get { return ConfigurationManager.AppSettings["RecipePuppyWebApiUrl"]; }
        }

        public static string RecipePuppyWebApiUrlWithQuery
        {
            get { return ConfigurationManager.AppSettings["RecipePuppyWebApiUrlWithQuery"]; }
        }
    }
    public static class ApiUrls
    {
        public const string RecipePuppyGet = "http://www.recipepuppy.com/api/";
        public const string RecipePuppyGetWithQuery = "http://www.recipepuppy.com/api/";
    }
}
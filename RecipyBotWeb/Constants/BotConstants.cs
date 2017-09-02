using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RecipyBotWeb.Constants
{
    public static class BotConstants
    {
        public class PreDefinedActions
        {
            public const string NewRecipe = "New recipe";
            public const string TopNRecipes = "Top 5 recipes";
            public const string NewRecipeWith = "New recipe with";
            public const string NewRecipeFor = "New recipe for";
            public const string NewRecipeGif = "New recipe gif";

            public const string Help = "Help";
            public const string About = "About";
            public const string GetStarted = "Get started";
        }

        public class BotApiSettings
        {
            public static string RecipePuppyWebApiUrl
            {
                get { return ConfigurationManager.AppSettings["Bot:RecipePuppyWebApiUrl"]; }
            }

            public static string RecipePuppyWebApiUrlWithQuery
            {
                get { return ConfigurationManager.AppSettings["Bot:RecipePuppyWebApiUrlWithQuery"]; }
            }

            public static string RecipePuppyWebApiUrlWithIngredients
            {
                get { return ConfigurationManager.AppSettings["Bot:RecipePuppyWebApiUrlWithIngredients"]; }
            }

            public static string RecipePuppyWebApiUrlWithPage
            {
                get { return ConfigurationManager.AppSettings["Bot:RecipePuppyWebApiUrlWithPage"]; }
            }

            public static string GifRecipes
            {
                get { return ConfigurationManager.AppSettings["Bot:GifRecipes"]; }
            }
        }

        public class ShortPreDefinedActions
        {
            public const string GifRecipe = "gif";
        }

        public class OtherConstants
        {
            public const int DefaultTopN = 5;
            public const string DefaultRecipeDish = "Chicken soup";
            public static readonly string[] DefaultIngredients = { "Chicken", "Carrots" };
            public const string GifImgurKeyword = "i.imgur.com";
            public const int MaxOptionsGives = 3;
        }
    }
}
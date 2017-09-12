using System.Configuration;

namespace RecipyBotWeb.Constants
{
    public static class BotConstants
    {
        /// <summary>
        /// Collection of pre defined actions that can be used as commands in the bot
        /// </summary>                  
        public class PreDefinedActions
        {
            public const string NewRecipe = "New recipe";
            public const string TopNRecipes = "Show me the top 5 recipes";
            public const string NewRecipeWith = "Show me a recipe with Chicken and carrots";
            public const string NewRecipeFor = "Show me a recipe for chicken soup";
            public const string NewRecipeGif = "Show me an animated recipe";
            public const string NewRandomRecipe = "Show me a recipe";

            public const string Help = "Help";
            public const string About = "About";
            public const string GetStarted = "Get started";
            public const string GifRecipe = "Gif";
            public const string Version = "Version";

            public const string VisitRecipeActionButton = "View this recipe";
        }
        
        /// <summary>
        /// Other bot level constants required for different purposes
        /// </summary>
        public class OtherConstants
        {
            public const int DefaultTopN = 5;
            public const string DefaultRecipeDish = "Chicken soup";
            public static readonly string[] DefaultIngredients = { "Chicken", "Carrots" };
            public const string DefaultIngredientsSerialized = "Chicken, Carrots";
            public const string GifImgurKeyword = "i.imgur.com";
            public const int MaxOptionsGives = 3;

            public static string Version
            {
                get { return ConfigurationManager.AppSettings["Bot:Version"]; }
            }
        }

        /// <summary>
        /// Api.Ai console intents
        /// </summary>
        public class ApiAiActionConstants
        {
            public const string RecipyOfTheDay = "recipy.oftheday";
            public const string RecipyCookFor = "recipy.cookfor";
            public const string RecipyCookWith = "recipy.cookwith";
            public const string RecipyTopN = "recipy.topn";
            public const string RecipyRandom = "recipy.random";
            public const string RecipyShowGif = "recipy.showgif";
        }

        /// <summary>
        /// Api.Ai parameters 
        /// </summary>
        public class ApiAiParametersConstants
        {
            public const string Number = "number";
            public const string FoodItem = "foodItem";
            public const string Recipe = "recipe"; 
        }

        /// <summary>
        /// Bot Api keys, URLs and other settings
        /// </summary>
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

            public static string ApiAiClientAccessToken
            {
                get { return ConfigurationManager.AppSettings["Bot:ApiAiClientAccessToken"]; }
            }

            public static string QwantImageApi
            {
                get { return ConfigurationManager.AppSettings["Bot:QwantImageApi"]; }
            }

            public static string FlickrImageApi
            {
                get { return ConfigurationManager.AppSettings["Bot:FlickrImageApi"]; }
            }

            public static string WebChatSecret
            {
                get { return ConfigurationManager.AppSettings["Bot:WebChatSecret"]; }
            }
        }       
    }
}
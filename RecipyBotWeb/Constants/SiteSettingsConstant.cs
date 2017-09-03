using System.Configuration;

namespace RecipyBotWeb.Constants
{
    public class SiteSettingsConstant
    {
        public static string FacebookBotUrl
        {
            get { return ConfigurationManager.AppSettings["FacebookBotUrl"]; }
        }

        public static string SkypeBotUrl
        {
            get { return ConfigurationManager.AppSettings["SkypeBotUrl"]; }
        }

        public static string SlackBotUrl
        {
            get { return ConfigurationManager.AppSettings["SlackBotUrl"]; }
        }

        public static string TelegramBotUrl
        {
            get { return ConfigurationManager.AppSettings["TelegramBotUrl"]; }
        }

        public static string WebBotUrl
        {
            get { return ConfigurationManager.AppSettings["WebBotUrl"]; }
        }

        public static string BotId
        {
            get { return ConfigurationManager.AppSettings["BotId"]; }
        }

        private static string _MicrosoftAppId
        {
            get { return ConfigurationManager.AppSettings["MicrosoftAppId"]; }
        }

        private static string _MicrosoftAppPassword
        {
            get { return ConfigurationManager.AppSettings["MicrosoftAppPassword"]; }
        }

    }
}
using System.Configuration;

namespace RecipyBotWeb.Constants
{
    public class SiteSettingsConstant
    {
        #region BOT URLS

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

        public static bool IsSkypeBotConfigured
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SkypeBotUrl"]); }
        }

        public static bool IsFacebookBotConfigured
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["FacebookBotUrl"]); }
        }

        public static bool IsSlackBotConfigured
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SlackBotUrl"]); }
        }

        public static bool IsTelegramBotConfigured
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["TelegramBotUrl"]); }
        }

        public static bool IsAnyBotConfigured
        {
            get { return (IsFacebookBotConfigured || IsSkypeBotConfigured || IsSlackBotConfigured || IsTelegramBotConfigured); }
        }

        #endregion

        #region EMAILS

        public static string SendGridApiKey
        {
            get { return ConfigurationManager.AppSettings["SendGridApiKey"]; }
        }

        public static string GeneralToEmail
        {
            get { return ConfigurationManager.AppSettings["GeneralToEmail"]; }
        }

        public static string GeneralFromEmail
        {
            get { return ConfigurationManager.AppSettings["GeneralFromEmail"]; }
        }

        public static string ContactUsSubjectLine
        {
            get { return ConfigurationManager.AppSettings["ContactUsSubjectLine"]; }
        }

        #endregion

        #region MS BOT FRAMEWORK

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

        #endregion

    }
}
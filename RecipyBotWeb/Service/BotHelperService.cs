using Microsoft.Bot.Connector;
using RecipyBotWeb.Constants;

namespace RecipyBotWeb.Service
{
    public class BotHelperService
    {
        #region CHECKS ON THE USER MESSAGE
        /// <summary>
        /// Returns true if user message is a pre-defined GIF request.
        /// </summary>
        public static bool IsGifRecipyRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.NewRecipeGif) || MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.GifRecipe);
        }

        /// <summary>
        /// Returns true if user message is a pre-defined random recipe request.
        /// </summary>
        public static bool IsRandomRecipeRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.NewRandomRecipe);
        }

        /// <summary>
        /// Returns true if user message is a pre-defined new recipe request.
        /// </summary>
        public static bool IsNewRecipeForRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.NewRecipeFor);
        }

        /// <summary>
        /// Returns true if user message is a pre-defined new recipe with ingredients request.
        /// </summary>
        public static bool IsNewRecipeWithRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.NewRecipeWith);
        }

        /// <summary>
        /// Returns true if user message is a pre-defined top N recipes request.
        /// </summary>
        public static bool IsTopNRecipesRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.TopNRecipes);
        }

        /// <summary>
        /// Returns true if user message is a pre-defined about us request.
        /// </summary>
        public static bool IsAboutRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.About);
        }

        /// <summary>
        /// Returns true if user message is a pre-defined get started request.
        /// </summary>
        public static bool IsHelpOrGetStartedRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.GetStarted) || MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.Help);
        }

        /// <summary>
        /// Returns true if user message is a pre-defined version request.
        /// </summary>
        public static bool IsVersionRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.Version);
        }

        /// <summary>
        /// Returns true if user message is a pre-defined feedback request.
        /// </summary>
        public static bool IsFeedbackRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.Feedback);
        }
        #endregion


        /// <summary>
        /// Returns the username greeting string.
        /// If an empty or default username is supplied, it will return an empty string.
        /// If an actual username is supplied, it returns the username+greetings.
        /// </summary>
        public static string GetUsernameValue(string username)
        {
            return MiscService.IsUserNameDefaultOrBlank(username) ? "" :  string.Format("Hi {0}! ", username.Split(' ')[0]);
        }


        #region PRE DEFINED RESPONSES
        /// <summary>
        /// Returns version information back to the user.
        /// </summary>
        public static Activity VersionResponse(Activity message)
        {
            Activity replyToConversation = message.CreateReply("The Recipy Bot is currently running at version " + BotConstants.OtherConstants.Version);
            return replyToConversation;
        }

        /// <summary>
        /// Returns the pre-defined get started response.
        /// </summary>
        public static Activity GetStartedResponse(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }

            string userGreeting = GetUsernameValue(message.From.Name);
            Activity replyToConversation = message.CreateReply(userGreeting + "To get started, simple type something like the following:\n\n * Show me the top 5 recipes\n\n * Show me a recipe with chicken and basil\n\n * Show me a recipe for risotto\n\n * Show me todays special.\n\n Recipy Bot is always online so feel free to send me a message anytime.");
            return replyToConversation;
        }

        /// <summary>
        /// Returns the pre-defined about us response.
        /// </summary>
        public static Activity AboutResponse(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }

            string response = "The Recipy Bot is your virtual assistant that helps you in your mundane task of deciding what to cook. Just ask The Recipy Bot what you would like a recipe for or get ideas for recipes that include the ingredients you have on you.";
            BotService.SendATextResponse(message, response);
            Activity replyToConversation = message.CreateReply("This is developed by [Clyde D'Souza](https://clydedsouza.net).\n\nRecipes provided by [Recipe Puppy](http://www.recipepuppy.com/).");
            return replyToConversation;
        }

        /// <summary>
        /// Returns the pre-defined feedback response.
        /// </summary>
        public static Activity FeedbackResponse(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }

            string response = "We would love to hear your feedback. Please visit [our contact page](https://recipybot.azurewebsites.net/contact) to send us your feedback.";
            BotService.SendATextResponse(message, response);
            Activity replyToConversation = message.CreateReply("If you spot any issues that you would like to raise, please visit [this page](https://github.com/ClydeDz/RecipyBot/issues/new).");
            return replyToConversation;
        }
        #endregion 

    }
}
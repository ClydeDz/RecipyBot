using System;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RecipyBotWeb.Service
{
    public static class BotService
    {
        public static Activity HandleBotRequest(Activity userMessage)
        {
            try
            {
                if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.NewRecipeGif) || MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.GifRecipe))
                {
                    // Sends a GIF recipe
                    return RecipePuppyService.GetRecipeGif(userMessage, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.NewRandomRecipe))
                {
                    // Sends a random recipe
                    return RecipePuppyService.GetRandomRecipe(userMessage, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.NewRecipeFor))
                {
                    // Sends a recipe for a particular item queried
                    return RecipePuppyService.GetRecipeFor(userMessage, BotConstants.OtherConstants.DefaultRecipeDish, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.NewRecipeWith))
                {
                    // Sends a recipe for the ingredients queried
                    return RecipePuppyService.GetRecipeWith(userMessage, BotConstants.OtherConstants.DefaultIngredients, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.TopNRecipes))
                {
                    // Send the top N recipes where N is a number
                    return RecipePuppyService.GetTopNRecipes(userMessage, BotConstants.OtherConstants.DefaultTopN, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.About))
                {
                    // Return the about us message
                    return AboutResponse(userMessage, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.GetStarted) || MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.Help))
                {
                    // Returns the get started/help generic message
                    return GetStartedResponse(userMessage, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.Version))
                {
                    // Returns the version response
                    return VersionResponse(userMessage);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.Feedback))
                {
                    // Returns the feedback response
                    return FeedbackResponse(userMessage, string.Empty);
                }
                else
                {
                    // Send the queries message to the API.AI handler for further processing
                    return ApiAiService.HandleNaturalInput(userMessage);
                }
            }
            catch(Exception e)
            {
                return userMessage.CreateReply("Oops, something went wrong " + e.Message);
            }
                    
        }

        #region BOT COMMUNICATION

        public static async void SendATextResponse(Activity userMessage, string responseToUser)
        {
            Activity reply = userMessage.CreateReply(responseToUser);
            await SendUserResponse(userMessage, reply);
        }

        public static async Task<ResourceResponse> SendUserResponse(Activity userMessage, Activity response)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(userMessage.ServiceUrl));
            if (response != null)
            {
                await connector.Conversations.ReplyToActivityAsync(response);
            }
            return null;
        }

        #endregion


        #region PRE DEFINED RESPONSES

        public static Activity VersionResponse(Activity message)
        {
            Activity replyToConversation = message.CreateReply("The Recipy Bot is currently running at version " + BotConstants.OtherConstants.Version);
            return replyToConversation;
        }

        public static Activity GetStartedResponse(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                SendATextResponse(message, defaultResponse);
            }

            Debug.WriteLine("Get Started Debug Writeline");

            string userGreeting = MiscService.IsUserNameDefaultOrBlank(message.From.Name) ? "Hi " + message.From.Name.Split(' ')[0] + "! " : string.Empty;
            Activity replyToConversation = message.CreateReply(userGreeting + "To get started, simple type something like the following:\n\n * Show me the top 5 recipes\n\n * Show me a recipe with chicken and basil\n\n * Show me a recipe for risotto\n\n * Show me todays special.\n\n Recipy Bot is always online so feel free to send me a message anytime.");
            return replyToConversation;
        }

        public static Activity AboutResponse(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                SendATextResponse(message, defaultResponse);
            }

            string response = "The Recipy Bot is your virtual assistant that helps you in your mundane task of deciding what to cook. Just ask The Recipy Bot what you would like a recipe for or get ideas for recipes that include the ingredients you have on you.";
            SendATextResponse(message, response);

            Activity replyToConversation = message.CreateReply("This is developed by [Clyde D'Souza](https://clydedsouza.net).\n\nRecipes provided by [Recipe Puppy](http://www.recipepuppy.com/).");
            return replyToConversation;
        }      

        public static Activity FeedbackResponse(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                SendATextResponse(message, defaultResponse);
            }

            string response = "We would love to hear your feedback. Please visit [our contact page](https://recipybot.azurewebsites.net/contact) to send us your feedback.";
            SendATextResponse(message, response);

            Activity replyToConversation = message.CreateReply("If you spot any issues that you would like to raise, please visit [this page](https://github.com/ClydeDz/RecipyBot/issues/new).");
            return replyToConversation;
        }

        #endregion 


    }
}
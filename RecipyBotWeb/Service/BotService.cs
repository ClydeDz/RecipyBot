using System;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;

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
                    return RecipePuppyService.GetRecipeGif(userMessage, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.NewRandomRecipe))
                {
                    return RecipePuppyService.GetRandomRecipe(userMessage, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.NewRecipeFor))
                {
                    return RecipePuppyService.GetRecipeFor(userMessage, BotConstants.OtherConstants.DefaultRecipeDish, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.NewRecipeWith))
                {
                    return RecipePuppyService.GetRecipeWith(userMessage, BotConstants.OtherConstants.DefaultIngredients, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.TopNRecipes))
                {
                    return RecipePuppyService.GetTopNRecipes(userMessage, BotConstants.OtherConstants.DefaultTopN, string.Empty);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.About))
                {
                    return AboutResponse(userMessage);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.GetStarted) || MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.Help))
                {
                    return GetStartedResponse(userMessage);
                }
                else if (MiscService.CompareTwoStrings(userMessage.Text, BotConstants.PreDefinedActions.Version))
                {
                    return VersionResponse(userMessage);
                }
                else
                {
                    return ApiAiService.HandleNaturalInput(userMessage);
                }
            }
            catch(Exception e)
            {
                return userMessage.CreateReply("Oops, something went wrong " + e.Message);
            }
                    
        }

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

        #region PRE DEFINED RESPONSES

        public static Activity GetStartedResponse(Activity message)
        {
            string user = MiscService.IsUserNameDefaultOrBlank(message.From.Name) ? "Hi " + message.From.Name + "! " : string.Empty;
            string response = "To get started, simple type something like the following:\n\n * Show me the top 5 recipes\n\n * Show me a recipe with chicken and basil\n\n * Show me a recipe for risotto\n\n * Show me an animated recipe\n\n * Show me a random recipe";
            SendATextResponse(message, response);

            Activity replyToConversation = message.CreateReply("Recipy Bot is always online so feel free to send me a message anytime.");
            return replyToConversation;
        }

        public static Activity AboutResponse(Activity message)
        {
            string response = "Recipy Bot is a virtual service that helps with fetching recipes for a particular dish or something that you can make using some ingredients that you may have.";
            SendATextResponse(message, response);

            Activity replyToConversation = message.CreateReply("This is developed by [Clyde D'Souza](https://clydedsouza.net). Please visit [this page](https://github.com/ClydeDz/RecipyBot/issues/new) to provide any feedback on this app.\n\nRecipes provided by [Recipe Puppy](http://www.recipepuppy.com/).");
            return replyToConversation;
        }

        public static Activity VersionResponse(Activity message)
        {
            Activity replyToConversation = message.CreateReply(BotConstants.OtherConstants.Version);
            return replyToConversation;
        }

        #endregion 


    }
}
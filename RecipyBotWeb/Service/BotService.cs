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
            if (userMessage.Text == BotConstants.PreDefinedActions.NewRecipeGif || userMessage.Text == BotConstants.PreDefinedActions.GifRecipe)
            {
                return RecipePuppyService.GetRecipeGif(userMessage); 
            }
            else if (userMessage.Text == BotConstants.PreDefinedActions.NewRandomRecipe)
            {
                return RecipePuppyService.GetRandomRecipe(userMessage);
            }
            else if (userMessage.Text == BotConstants.PreDefinedActions.NewRecipeFor)
            {
                return RecipePuppyService.GetRecipeFor(userMessage, BotConstants.OtherConstants.DefaultRecipeDish);
            }
            else if (userMessage.Text == BotConstants.PreDefinedActions.NewRecipeWith)
            {
                return RecipePuppyService.GetRecipeWith(userMessage, BotConstants.OtherConstants.DefaultIngredients);
            }
            else if (userMessage.Text == BotConstants.PreDefinedActions.TopNRecipes)
            {
                return RecipePuppyService.GetTopNRecipes(userMessage, BotConstants.OtherConstants.DefaultTopN);
            }
            else if (userMessage.Text == BotConstants.PreDefinedActions.About)
            {
                return AboutResponse(userMessage);
            }
            else if (userMessage.Text == BotConstants.PreDefinedActions.Help)
            {
                return HelpResponse(userMessage);
            }
            else if (userMessage.Text == BotConstants.PreDefinedActions.GetStarted)
            {
                return GetStartedResponse(userMessage);
            }
            else
            {
                return ApiAiService.HandleNaturalInput(userMessage);
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
            Activity replyToConversation = message.CreateReply("To get started, simple type \n\n New recipe gif or \n\n random");
            return replyToConversation;
        }

        public static Activity HelpResponse(Activity message)
        {
            Activity replyToConversation = message.CreateReply("Help To get started, simple type \n New recipe gif or \n random");
            return replyToConversation;
        }

        public static Activity AboutResponse(Activity message)
        {
            Activity replyToConversation = message.CreateReply("About To get started, simple type \n New recipe gif or \n random");
            return replyToConversation;
        }

        #endregion 


    }
}
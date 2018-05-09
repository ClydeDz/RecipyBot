using System;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RecipyBotWeb.Service
{
    public static class BotService
    {
        /// <summary>
        /// Handles the incoming message from the user.
        /// Checks if its a standard pre-defined message from the user and handles accordingly.
        /// If not, then it passes the message to the Dialogflow (API.AI) service to decode.
        /// </summary>
        public static Activity HandleBotRequest(Activity userMessage)
        {
            try
            {
                if (BotHelperService.IsGifRecipyRequest(userMessage.Text))
                {
                    // Sends a GIF recipe
                    return RecipePuppyService.GetRecipeGif(userMessage, string.Empty);
                }
                else if (BotHelperService.IsRandomRecipeRequest(userMessage.Text))
                {
                    // Sends a random recipe
                    return RecipePuppyService.GetRandomRecipe(userMessage, string.Empty);
                }
                else if (BotHelperService.IsNewRecipeForRequest(userMessage.Text))
                {
                    // Sends a recipe for a particular item queried
                    return RecipePuppyService.GetRecipeFor(userMessage, BotConstants.OtherConstants.DefaultRecipeDish, string.Empty);
                }
                else if (BotHelperService.IsNewRecipeWithRequest(userMessage.Text))
                {
                    // Sends a recipe for the ingredients queried
                    return RecipePuppyService.GetRecipeWith(userMessage, BotConstants.OtherConstants.DefaultIngredients, string.Empty);
                }
                else if (BotHelperService.IsTopNRecipesRequest(userMessage.Text))
                {
                    // Send the top N recipes where N is a number
                    return RecipePuppyService.GetTopNRecipes(userMessage, BotConstants.OtherConstants.DefaultTopN, string.Empty);
                }
                else if (BotHelperService.IsAboutRequest(userMessage.Text))
                {
                    // Return the about us message
                    return BotHelperService.AboutResponse(userMessage, string.Empty);
                }
                else if (BotHelperService.IsHelpOrGetStartedRequest(userMessage.Text))
                {
                    // Returns the get started/help generic message
                    return BotHelperService.GetStartedResponse(userMessage, string.Empty);
                }
                else if (BotHelperService.IsVersionRequest(userMessage.Text))
                {
                    // Returns the version response
                    return BotHelperService.VersionResponse(userMessage);
                }
                else if (BotHelperService.IsFeedbackRequest(userMessage.Text))
                {
                    // Returns the feedback response
                    return BotHelperService.FeedbackResponse(userMessage, string.Empty);
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
        /// <summary>
        /// Creates a response message and passes it on to the send response method.
        /// </summary>
        public static async void SendATextResponse(Activity userMessage, string responseToUser)
        {
            Activity reply = userMessage.CreateReply(responseToUser);
            await SendUserResponse(userMessage, reply);
        }

        /// <summary>
        /// Send the response back to the user.
        /// </summary>
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

    }
}
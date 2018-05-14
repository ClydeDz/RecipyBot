using ApiAiSDK;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;
using System;
using Newtonsoft.Json;
using ApiAiSDK.Model;

// Now Dialogflow.com

namespace RecipyBotWeb.Service
{
    public class ApiAiService
    {
        /// <summary>
        /// Handles other non-pre-defined messages in the bot.
        /// These messages are passed to the DialogFlow API to process.
        /// </summary>
        public static Activity HandleNaturalInput(Activity message)
        {
            try
            {
                var response = SendRequestToApiAi(message);
                switch (response.Result.Action)
                {
                    // Returns the recipe for an item searched
                    case BotConstants.ApiAiActionConstants.RecipyCookFor:
                        var entityRecipyCookFor = MiscService.GetFoodEntities(response.Result.Parameters, BotConstants.FoodEntitiesEnum.Recipe);
                        if (string.IsNullOrEmpty(entityRecipyCookFor))
                        {
                            return message.CreateReply(response.Result.Fulfillment.Speech);
                        }
                        return RecipePuppyService.GetRecipeFor(message, entityRecipyCookFor, response.Result.Fulfillment.Speech);

                    // Returns the recipe of the day
                    case BotConstants.ApiAiActionConstants.RecipyOfTheDay:
                        return RecipePuppyService.GetRandomRecipe(message, response.Result.Fulfillment.Speech);

                    // Returns a random recipe
                    case BotConstants.ApiAiActionConstants.RecipyRandom:
                        return RecipePuppyService.GetRandomRecipe(message, response.Result.Fulfillment.Speech);

                    // Return a recipe for the ingredients queried
                    case BotConstants.ApiAiActionConstants.RecipyCookWith:
                        var entityRecipyCookWith = MiscService.GetFoodEntities(response.Result.Parameters, BotConstants.FoodEntitiesEnum.FoodItem);
                        if (string.IsNullOrEmpty(entityRecipyCookWith))
                        {
                            return message.CreateReply(response.Result.Fulfillment.Speech);
                        }
                        return RecipePuppyService.GetRecipeWith(message, JsonConvert.DeserializeObject<string[]>(entityRecipyCookWith), response.Result.Fulfillment.Speech);

                    // Returns a GIF recipe
                    case BotConstants.ApiAiActionConstants.RecipyShowGif:
                        return RecipePuppyService.GetRecipeGif(message, response.Result.Fulfillment.Speech);

                    // Returns top N recipes where N is a number
                    case BotConstants.ApiAiActionConstants.RecipyTopN:
                        return RecipePuppyService.GetTopNRecipes(message, MiscService.GetNumericEntity(response.Result.Parameters), response.Result.Fulfillment.Speech);

                    // Returns a generic about response
                    case BotConstants.ApiAiActionConstants.GeneralAbout:
                        return BotHelperService.AboutResponse(message, response.Result.Fulfillment.Speech);

                    // Returns a generic help/get started response
                    case BotConstants.ApiAiActionConstants.GeneralGetStarted:
                        return BotHelperService.GetStartedResponse(message, response.Result.Fulfillment.Speech);

                    // Returns a generic help/get started response
                    case BotConstants.ApiAiActionConstants.GeneralHelp:
                        return BotHelperService.GetStartedResponse(message, response.Result.Fulfillment.Speech);

                    // Returns a generic version response
                    case BotConstants.ApiAiActionConstants.GeneralVersion:
                        return BotHelperService.VersionResponse(message);

                    // Returns a generic feedback response
                    case BotConstants.ApiAiActionConstants.GeneralFeedback:
                        return BotHelperService.FeedbackResponse(message, response.Result.Fulfillment.Speech);
                    
                    // Returns a response from API.AI for general smalltalk
                    case BotConstants.ApiAiActionConstants.GeneralSmallTalk:
                        return message.CreateReply(response.Result.Fulfillment.Speech);

                    // Returns a response from API.AI directly
                    default:
                        return message.CreateReply(response.Result.Fulfillment.Speech);
                }
            }
            catch (Exception e)
            {
                return message.CreateReply("Oops, something happened " + e.Message);
            }
        }

        /// <summary>
        /// Sends the message to the DialogFlow API.
        /// </summary>
        public static AIResponse SendRequestToApiAi(Activity message)
        {
            var apiAi = new ApiAi(new AIConfiguration(BotConstants.BotApiSettings.ApiAiClientAccessToken, SupportedLanguage.English));
            return apiAi.TextRequest(message.Text);
        }
    }
}
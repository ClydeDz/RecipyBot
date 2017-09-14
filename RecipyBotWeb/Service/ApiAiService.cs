using ApiAiSDK;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;
using System;
using Newtonsoft.Json;

namespace RecipyBotWeb.Service
{
    public class ApiAiService
    {
        public static Activity HandleNaturalInput(Activity message)
        {
            try
            {
                var apiAi = new ApiAi(new AIConfiguration(BotConstants.BotApiSettings.ApiAiClientAccessToken, SupportedLanguage.English));
                var response = apiAi.TextRequest(message.Text);

                switch (response.Result.Action)
                {
                    // Returns the recipe for an item searched
                    case BotConstants.ApiAiActionConstants.RecipyCookFor:
                        var entityRecipyCookFor = MiscService.GetFoodEntities(response.Result.Parameters);
                        if (string.IsNullOrEmpty(entityRecipyCookFor))
                        {
                            return message.CreateReply(response.Result.Fulfillment.Speech);
                        }
                        return RecipePuppyService.GetRecipeFor(message, MiscService.GetFoodEntities(response.Result.Parameters), response.Result.Fulfillment.Speech);

                    // Returns the recipe of the day
                    case BotConstants.ApiAiActionConstants.RecipyOfTheDay:
                        return RecipePuppyService.GetRandomRecipe(message, response.Result.Fulfillment.Speech);

                    // Returns a random recipe
                    case BotConstants.ApiAiActionConstants.RecipyRandom:
                        return RecipePuppyService.GetRandomRecipe(message, response.Result.Fulfillment.Speech);

                    // Return a recipe for the ingredients queried
                    case BotConstants.ApiAiActionConstants.RecipyCookWith:
                        var entityRecipyCookWith = MiscService.GetFoodEntities(response.Result.Parameters);
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
                        return BotService.AboutResponse(message, response.Result.Fulfillment.Speech);

                    // Returns a generic help/get started response
                    case BotConstants.ApiAiActionConstants.GeneralGetStarted:
                        return BotService.GetStartedResponse(message, response.Result.Fulfillment.Speech);

                    // Returns a generic help/get started response
                    case BotConstants.ApiAiActionConstants.GeneralHelp:
                        return BotService.GetStartedResponse(message, response.Result.Fulfillment.Speech);

                    // Returns a generic version response
                    case BotConstants.ApiAiActionConstants.GeneralVersion:
                        return BotService.VersionResponse(message);

                    // Returns a generic feedback response
                    case BotConstants.ApiAiActionConstants.GeneralFeedback:
                        return BotService.FeedbackResponse(message, response.Result.Fulfillment.Speech);

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
    }
}
using ApiAiSDK;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                    case BotConstants.ApiAiActionConstants.RecipyCookFor:
                        var entityRecipyCookFor = GetFoodEntities(response.Result.Parameters);
                        if (string.IsNullOrEmpty(entityRecipyCookFor))
                        {
                            return message.CreateReply(response.Result.Fulfillment.Speech);
                        }
                        return RecipePuppyService.GetRecipeFor(message, GetFoodEntities(response.Result.Parameters), response.Result.Fulfillment.Speech);

                    case BotConstants.ApiAiActionConstants.RecipyOfTheDay:
                        return RecipePuppyService.GetRandomRecipe(message, response.Result.Fulfillment.Speech);

                    case BotConstants.ApiAiActionConstants.RecipyRandom:
                        return RecipePuppyService.GetRandomRecipe(message, response.Result.Fulfillment.Speech);

                    case BotConstants.ApiAiActionConstants.RecipyCookWith:
                        var entityRecipyCookWith = GetFoodEntities(response.Result.Parameters);
                        if (string.IsNullOrEmpty(entityRecipyCookWith))
                        {
                            return message.CreateReply(response.Result.Fulfillment.Speech);
                        }
                        return RecipePuppyService.GetRecipeWith(message, JsonConvert.DeserializeObject<string[]>(entityRecipyCookWith), response.Result.Fulfillment.Speech);

                    case BotConstants.ApiAiActionConstants.RecipyShowGif:
                        return RecipePuppyService.GetRecipeGif(message, response.Result.Fulfillment.Speech);

                    case BotConstants.ApiAiActionConstants.RecipyTopN:
                        return RecipePuppyService.GetTopNRecipes(message, GetNumericEntity(response.Result.Parameters), response.Result.Fulfillment.Speech);

                    default:
                        return message.CreateReply(response.Result.Fulfillment.Speech);
                }
            }
            catch (Exception e)
            {
                return message.CreateReply("Oops, something happened " + e.Message);
            }
        }

        private static int GetNumericEntity(Dictionary<string, object> paramters)
        {
            int x = 0;
            foreach (var j in paramters)
            {
                if (j.Key == BotConstants.ApiAiParametersConstants.Number)
                {
                    x = Convert.ToInt32(j.Value ?? BotConstants.OtherConstants.DefaultTopN);
                }
            }
            return x;
        }

        private static string GetFoodEntities(Dictionary<string, object> paramters)
        {
            string foodItem = string.Empty;
            foreach (var j in paramters)
            {
                if (j.Key == BotConstants.ApiAiParametersConstants.FoodItem)
                {
                    foodItem = string.IsNullOrEmpty(j.Value.ToString()) ? BotConstants.OtherConstants.DefaultIngredientsSerialized : j.Value.ToString();
                }
                if (j.Key == BotConstants.ApiAiParametersConstants.Recipe)
                {
                    foodItem = string.IsNullOrEmpty(j.Value.ToString()) ? BotConstants.OtherConstants.DefaultRecipeDish : j.Value.ToString();
                }
            }
            return foodItem;
        }
    }
}
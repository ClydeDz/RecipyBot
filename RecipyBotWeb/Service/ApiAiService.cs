using ApiAiSDK;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;

namespace RecipyBotWeb.Service
{
    public class ApiAiService
    {
        public static Activity HandleNaturalInput(Activity message)
        {
            var config = new AIConfiguration(BotConstants.BotApiSettings.ApiAiClientAccessToken, SupportedLanguage.English);
            var apiAi = new ApiAi(config);
            var response = apiAi.TextRequest(message.Text);
            if (!string.IsNullOrEmpty(response.Result.Fulfillment.Speech) && !IsSmallTalk(response.Result.Action))
            {
                BotService.SendATextResponse(message, response.Result.Fulfillment.Speech);
            }            

            switch (response.Result.Action)
            {
                case BotConstants.ApiAiActionConstants.RecipyCookFor:
                    return RecipePuppyService.GetTopNRecipes(message, 5);

                case BotConstants.ApiAiActionConstants.RecipyOfTheDay:                   
                    return RecipePuppyService.GetRandomRecipe(message);

                case BotConstants.ApiAiActionConstants.RecipyRandom:
                    return RecipePuppyService.GetRandomRecipe(message);

                case BotConstants.ApiAiActionConstants.RecipyCookWith:
                    return RecipePuppyService.GetRandomRecipe(message);

                case BotConstants.ApiAiActionConstants.RecipyShowGif:
                    return RecipePuppyService.GetRecipeGif(message);

                case BotConstants.ApiAiActionConstants.RecipyTopN:
                    return RecipePuppyService.GetTopNRecipes(message, 5);

                default:
                    return message.CreateReply(response.Result.Fulfillment.Speech);
            }            
        }

        public static bool IsSmallTalk(string apiAiAction)
        {
            return apiAiAction.Contains("smalltalk");
        }
    }
}
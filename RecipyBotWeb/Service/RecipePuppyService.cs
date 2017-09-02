using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipyBotWeb.Models;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;

namespace RecipyBotWeb.Service
{
    public class RecipePuppyService
    {
        public static Activity GetAllRecipies(Activity message)
        {
            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrl);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(webResponse.results.Count(), webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers);
        }

        public static Activity GetRandomRecipe(Activity message)
        {
            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrl);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(1, webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers);
        }

        public static Activity GetTopNRecipes(Activity message, int n)
        {
            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrl);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(n, webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers);
        }

        public static Activity GetRecipeWith(Activity message, string[] ingredients)
        {
            string listOfIngredients = string.Join(",", ingredients.Select(item => item).ToArray());
            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrlWithIngredients + "" + listOfIngredients);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(BotConstants.OtherConstants.MaxOptionsGives, webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers);
        }

        public static Activity GetRecipeFor(Activity message, string dish)
        {
            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrlWithQuery + "" + dish);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(BotConstants.OtherConstants.MaxOptionsGives, webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers);            
        }

        public static Activity GetRecipeGif(Activity message)
        {
            GifRecipesDataModel webResponse = WebApiConnectorService.GenericGetRequest<GifRecipesDataModel>(BotConstants.BotApiSettings.GifRecipes);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(1, webResponse.data.children.Where(p=> p.data.domain == BotConstants.OtherConstants.GifImgurKeyword).Count());

            Activity replyToConversation = message.CreateReply();
            replyToConversation.Attachments = new List<Attachment>();
                        
            foreach(var value in randomNumbers)
            {
                var thisData = webResponse.data.children.Where(k => k.data.domain == BotConstants.OtherConstants.GifImgurKeyword).ElementAt(value);

                replyToConversation.Attachments.Add(
                    new AnimationCard
                    {
                        Title = thisData.data.title,
                        Subtitle = "",
                        Image = new ThumbnailUrl
                        {
                            Url = thisData.data.thumbnail
                        },
                        Media = new List<MediaUrl>
                        {
                            new MediaUrl()
                            {
                                Url = thisData.data.url
                            }
                        }
                    }.ToAttachment()
                );
            }
            
            return replyToConversation;
        }

        private static Activity _GenerateRecipeMessage(Activity message, RecipePuppyDataModel recipes, IEnumerable<int> randomNumbers)
        {   
            Activity replyToConversation = message.CreateReply();
            replyToConversation.AttachmentLayout = AttachmentLayoutTypes.List;
            replyToConversation.Attachments = new List<Attachment>();
            Dictionary<string, string> cardContentList = new Dictionary<string, string>();

            foreach (var value in randomNumbers)
            {
                var recipe = recipes.results.ElementAt(value);

                // Add basic card content details
                cardContentList.Add(
                    recipe.title,
                    recipe.href
                );

                // Create a card image element
                List<CardImage> cardImages = new List<CardImage>();
                cardImages.Add(
                    new CardImage(url: recipe.thumbnail)
                );

                // Create a card button element
                List<CardAction> cardButtons = new List<CardAction>();
                cardButtons.Add(
                    new CardAction()
                    {
                        Value = recipe.ingredients,
                        Type = "openUrl",
                        Title = recipe.title
                    }
                );

                // Add the card image and button element to a thumbnail of the response
                replyToConversation.Attachments.Add(
                    new ThumbnailCard()
                    {
                        Title = recipe.title,
                        Subtitle = recipe.ingredients,
                        Images = cardImages,
                        Buttons = cardButtons
                    }.ToAttachment()
                );
            }

            return replyToConversation;
        }
    }
}
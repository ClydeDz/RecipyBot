using System.Collections.Generic;
using System.Linq;
using RecipyBotWeb.Models;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;

namespace RecipyBotWeb.Service
{
    public class RecipePuppyService
    {
        public static Activity GetAllRecipies(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }

            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrl);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(webResponse.results.Count(), webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers, defaultResponse);
        }

        public static Activity GetRandomRecipe(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }

            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrl);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(1, webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers, defaultResponse);
        }

        public static Activity GetTopNRecipes(Activity message, int n, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }
           
            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrl);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(n, webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers, defaultResponse);
        }

        public static Activity GetRecipeWith(Activity message, string[] ingredients, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }

            string listOfIngredients = string.Join(",", ingredients.Select(item => item).ToArray());
            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrlWithIngredients + "" + listOfIngredients);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(BotConstants.OtherConstants.MaxOptionsGives, webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers, defaultResponse);
        }

        public static Activity GetRecipeFor(Activity message, string dish, string defaultResponse)
        {
            BotService.SendATextResponse(message, defaultResponse);
            RecipePuppyDataModel webResponse = WebApiConnectorService.GenericGetRequest<RecipePuppyDataModel>(BotConstants.BotApiSettings.RecipePuppyWebApiUrlWithQuery + "" + dish);
            IEnumerable<int> randomNumbers = MiscService.GiveXFromYNumbers(BotConstants.OtherConstants.MaxOptionsGives, webResponse.results.Count());
            return _GenerateRecipeMessage(message, webResponse, randomNumbers, defaultResponse);            
        }

        public static Activity GetRecipeGif(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }            

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
                        Subtitle = "by @" + thisData.data.author,
                        Text = "Perfect for " + thisData.data.link_flair_text.ToString().ToLower().Trim(),
                        Autostart = true,
                        Shareable = true,
                        Image = new ThumbnailUrl
                        {
                            Url = thisData.data.thumbnail
                        },
                        Media = new List<MediaUrl>
                        {
                            new MediaUrl()
                            {
                                Url = MiscService.MakeGif(thisData.data.url), Profile = "image/gif"
                            }
                        },
                        Buttons = new List<CardAction>
                        {
                            new CardAction()
                            {
                                Title = "Learn More",
                                Type = ActionTypes.OpenUrl,
                                Value = "https://peach.blender.org/"
                            }
                        }
                    }.ToAttachment()
                );
            }
            
            return replyToConversation;
        }

        private static Activity _GenerateRecipeMessage(Activity message, RecipePuppyDataModel recipes, IEnumerable<int> randomNumbers, string defaultResponse)
        {   
            Activity replyToConversation = message.CreateReply();
            replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyToConversation.Attachments = new List<Attachment>();
            Dictionary<string, string> cardContentList = new Dictionary<string, string>();

            foreach (var value in randomNumbers)
            {
                var recipe = recipes.results.ElementAt(value);
                string recipeImage = FlickrService.GetImage(recipe.title);

                // Add basic card content details
                cardContentList.Add(
                    recipe.title,
                    recipe.href
                );

                // Add the card image and button element to a thumbnail of the response
                replyToConversation.Attachments.Add(
                    new HeroCard() // Can also use HeroCard()
                    {
                        Title = recipe.title,
                        Subtitle = MiscService.GetRandomTagline(),
                        Text = MiscService.GetRandomIngredientsPrefix() + recipe.ingredients,
                        Images = new List<CardImage> { new CardImage(recipeImage) }, //recipe.thumbnail
                        Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, BotConstants.PreDefinedActions.VisitRecipeActionButton, value: recipe.href) }
                    }.ToAttachment()
                );
            }

            return replyToConversation;
        }


     
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipyBotWeb.Constants;
using Microsoft.Bot.Connector;

namespace RecipyBotWeb.Service
{
    public static class BotService
    {
        public static Activity MatchPreDefinedMessage(Activity userMessage)
        {
            if (userMessage.Text == BotConstants.PreDefinedActions.NewRecipeGif || userMessage.Text == BotConstants.ShortPreDefinedActions.GifRecipe)
            {
                return RecipePuppyService.GetRecipeGif(userMessage); 
            }
            else if (userMessage.Text == BotConstants.PreDefinedActions.NewRecipe)
            {
                return RecipePuppyService.GetAllRecipies(userMessage);
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
            return null;
        }

        #region PRE DEFINED RESPONSES

        public static Activity GetStartedResponse(Activity message)
        {
            Activity replyToConversation = message.CreateReply("To get started, simple type \n New recipe gif or \n random");
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
using System;
using Xunit;
using RecipyBotWeb.Service;
using Microsoft.Bot.Connector;

namespace RecipyBot.Test.Service
{
    public class BotHelperServiceTest
    {
        // IsGifRecipyRequest()
        
        [Theory(DisplayName = "IsGifRequest_ReturnsTrue_Test")]
        [InlineData("gif")]
        [InlineData("Gif")]
        [InlineData("GIF")]
        [InlineData("Show me an animated recipe")]
        [InlineData("show me an animated recipe")]
        [InlineData("SHOW ME AN ANIMATED RECIPE")]
        public void IsGifRequest_ReturnsTrue_Test(string userMessage)
        {
            Assert.True(BotHelperService.IsGifRecipyRequest(userMessage));
        }
        
        [Theory(DisplayName = "IsGifRequest_ReturnsFalse_Test")]
        [InlineData(".gif")]
        [InlineData("anime")]
        [InlineData("Show me a gif")]
        [InlineData("")]
        public void IsGifRequest_ReturnsFalse_Test(string userMessage)
        {
            Assert.False(BotHelperService.IsGifRecipyRequest(userMessage));
        }

        // IsRandomRecipeRequest()

        [Theory(DisplayName = "IsRandomRecipeRequest_ReturnsTrue_Test")]
        [InlineData("Show me a recipe")]
        [InlineData("show me a recipe")]
        public void IsRandomRecipeRequest_ReturnsTrue_Test(string userMessage)
        {
            Assert.True(BotHelperService.IsRandomRecipeRequest(userMessage));
        }

        [Theory(DisplayName = "IsRandomRecipeRequest_ReturnsFalse_Test")]
        [InlineData("Show me a recipy")]
        [InlineData("")]
        public void IsRandomRecipeRequest_ReturnsFalse_Test(string userMessage)
        {
            Assert.False(BotHelperService.IsRandomRecipeRequest(userMessage));
        }

        // IsNewRecipeForRequest()

        [Theory(DisplayName = "IsNewRecipeForRequest_ReturnsTrue_Test")]
        [InlineData("Show me a recipe for chicken soup")]
        [InlineData("show me a recipe for chicken soup")]
        public void IsNewRecipeForRequest_ReturnsTrue_Test(string userMessage)
        {
            Assert.True(BotHelperService.IsNewRecipeForRequest(userMessage));
        }

        [Theory(DisplayName = "IsNewRecipeForRequest_ReturnsFalse_Test")]
        [InlineData("Show me a recipe for fried rice")]
        [InlineData("")]
        public void IsNewRecipeForRequest_ReturnsFalse_Test(string userMessage)
        {
            Assert.False(BotHelperService.IsNewRecipeForRequest(userMessage));
        }

        // IsNewRecipeWithRequest()
        
        [Theory(DisplayName = "IsNewRecipeWithRequest_ReturnsTrue_Test")]
        [InlineData("Show me a recipe with Chicken and carrots")]
        [InlineData("show me a recipe with chicken and carrots")]
        public void IsNewRecipeWithRequest_ReturnsTrue_Test(string userMessage)
        {
            Assert.True(BotHelperService.IsNewRecipeWithRequest(userMessage));
        }

        [Theory(DisplayName = "IsNewRecipeWithRequest_ReturnsFalse_Test")]
        [InlineData("Show me a recipe with Chicken and beans")]
        [InlineData("")]
        public void IsNewRecipeWithRequest_ReturnsFalse_Test(string userMessage)
        {
            Assert.False(BotHelperService.IsNewRecipeWithRequest(userMessage));
        }

        // IsTopNRecipesRequest()

        [Theory(DisplayName = "IsTopNRecipesRequest_ReturnsTrue_Test")]
        [InlineData("Show me the top 5 recipes")]
        [InlineData("show me the top 5 recipes")]
        public void IsTopNRecipesRequest_ReturnsTrue_Test(string userMessage)
        {
            Assert.True(BotHelperService.IsTopNRecipesRequest(userMessage));
        }

        [Theory(DisplayName = "IsTopNRecipesRequest_ReturnsFalse_Test")]
        [InlineData("Show me the top 3 recipes")]
        [InlineData("")]
        public void IsTopNRecipesRequest_ReturnsFalse_Test(string userMessage)
        {
            Assert.False(BotHelperService.IsTopNRecipesRequest(userMessage));
        }


        // IsAboutRequest()

        [Theory(DisplayName = "IsAboutRequest_ReturnsTrue_Test")]
        [InlineData("About")]
        [InlineData("about")]
        public void IsAboutRequest_ReturnsTrue_Test(string userMessage)
        {
            Assert.True(BotHelperService.IsAboutRequest(userMessage));
        }

        [Theory(DisplayName = "IsAboutRequest_ReturnsFalse_Test")]
        [InlineData("About us")]
        [InlineData("")]
        public void IsAboutRequest_ReturnsFalse_Test(string userMessage)
        {
            Assert.False(BotHelperService.IsAboutRequest(userMessage));
        }

        // IsHelpOrGetStartedRequest()

        [Theory(DisplayName = "IsHelpOrGetStartedRequest_ReturnsTrue_Test")]
        [InlineData("Help")]
        [InlineData("help")]
        [InlineData("Get started")]
        [InlineData("get started")]
        public void IsHelpOrGetStartedRequest_ReturnsTrue_Test(string userMessage)
        {
            Assert.True(BotHelperService.IsHelpOrGetStartedRequest(userMessage));
        }

        [Theory(DisplayName = "IsHelpOrGetStartedRequest_ReturnsFalse_Test")]
        [InlineData("Help me")]
        [InlineData("started")]
        [InlineData("")]
        public void IsHelpOrGetStartedRequest_ReturnsFalse_Test(string userMessage)
        {
            Assert.False(BotHelperService.IsHelpOrGetStartedRequest(userMessage));
        }


        // IsVersionRequest()

        [Theory(DisplayName = "IsVersionRequest_ReturnsTrue_Test")]
        [InlineData("Version")]
        [InlineData("version")]
        public void IsVersionRequest_ReturnsTrue_Test(string userMessage)
        {
            Assert.True(BotHelperService.IsVersionRequest(userMessage));
        }

        [Theory(DisplayName = "IsVersionRequest_ReturnsFalse_Test")]
        [InlineData("Version number")]
        [InlineData("v")]
        [InlineData("")]
        public void IsVersionRequest_ReturnsFalse_Test(string userMessage)
        {
            Assert.False(BotHelperService.IsVersionRequest(userMessage));
        }


        // IsFeedbackRequest()

        [Theory(DisplayName = "IsFeedbackRequest_ReturnsTrue_Test")]
        [InlineData("Feedback")]
        [InlineData("feedback")]
        public void IsFeedbackRequest_ReturnsTrue_Test(string userMessage)
        {
            Assert.True(BotHelperService.IsFeedbackRequest(userMessage));
        }

        [Theory(DisplayName = "IsFeedbackRequest_ReturnsFalse_Test")]
        [InlineData("Feedback pls")]
        [InlineData("")]
        public void IsFeedbackRequest_ReturnsFalse_Test(string userMessage)
        {
            Assert.False(BotHelperService.IsFeedbackRequest(userMessage));
        }


        // GetUsernameValue()

        [Theory(DisplayName = "GetUsernameValue_ReturnsExpectedUsername_Test")]
        [InlineData("user", "")]
        [InlineData("User", "")]
        [InlineData("User ", "")]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("john", "Hi john! ")]
        [InlineData("Karen", "Hi Karen! ")]
        [InlineData("james s", "Hi james! ")]
        public void GetUsernameValue_ReturnsExpectedUsername_Test(string username, string expected)
        {
            Assert.Equal(expected, BotHelperService.GetUsernameValue(username));
        }

    }
}

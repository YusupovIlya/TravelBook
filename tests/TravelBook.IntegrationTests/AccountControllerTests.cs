using AngleSharp.Html.Dom;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using TravelBook.IntegrationTests.Helpers;

namespace TravelBook.IntegrationTests;

public class AccountControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("/account/login")]
    [InlineData("/account/register")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // arrange
        var client = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
    }



    [Fact]
    public async Task Post_Login_ReturnRedirectToAboutMe()
    {
        // Arrange
        string destPath = "/account/aboutme";
        var client = Factory.CreateClient();
        var defaultPage = await client.GetAsync($"/account/login");
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        // Act
        var response = await client.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            new Dictionary<string, string>
            {
                ["Email"] = "asds@gmail.com",
                ["Password"] = "7Cmv7HN2969aZWz",
                ["RememberMe"] = "false",
            });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(destPath, response.RequestMessage.RequestUri.LocalPath);
    }

    [Fact]
    public async Task Post_LoginWithWrongPassword_ReturnLoginPage()
    {
        // Arrange
        string destPath = "/";
        var client = Factory.CreateClient();
        var defaultPage = await client.GetAsync($"/account/login");
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        // Act
        var response = await client.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            new Dictionary<string, string>
            {
                ["Email"] = "asds@gmail.com",
                ["Password"] = "12345",
                ["RememberMe"] = "false",
            });

        // Assert
        Assert.Equal(destPath, response.RequestMessage.RequestUri.LocalPath);
    }

    [Fact]
    public async Task Post_Register_ReturnRedirectToAboutMe()
    {
        // Arrange
        string destPath = "/account/aboutme";
        var client = Factory.CreateClient();
        var defaultPage = await client.GetAsync($"/account/register");
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        // Act
        var response = await client.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            new Dictionary<string, string>
            {
                ["Email"] = "example@gmail.com",
                ["Password"] = "qwerty123",
                ["ConfirmPassword"] = "qwerty123",
            });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(destPath, response.RequestMessage.RequestUri.LocalPath);
    }
}

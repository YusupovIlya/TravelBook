using AngleSharp.Html.Dom;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using TravelBook.IntegrationTests.Helpers;
using Microsoft.AspNetCore.WebUtilities;

namespace TravelBook.IntegrationTests;

public class ArticlesControllerTests : BaseControllerTests
{
    const string destPath = "/articles/travelarticles";

    [Theory]
    [InlineData("10")]
    public async Task Post_CreateArticle_ReturnRedirectToTravelArticles(string travelId)
    {
        // Arrange
        var defaultPage = await AuthClientWithRedir.GetAsync($"/articles/create?travelId={travelId}");
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        var response = await AuthClientWithRedir.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            new Dictionary<string, string>
            {
                ["Title"] = "note #1",
                ["Text"] = "Some text",
            });
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(destPath, response.RequestMessage.RequestUri.LocalPath);
    }

    [Theory]
    [InlineData("8")]
    public async Task Post_EditArticle_ReturnRedirectToTravelArticles(string articleId)
    {
        // Arrange
        var defaultPage = await AuthClientWithRedir.GetAsync($"/articles/edit?articleId={articleId}");
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        var response = await AuthClientWithRedir.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            new Dictionary<string, string>
            {
                ["Title"] = "note #10",
                ["Text"] = "New some text",
            });
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(destPath, response.RequestMessage.RequestUri.LocalPath);
    }
}

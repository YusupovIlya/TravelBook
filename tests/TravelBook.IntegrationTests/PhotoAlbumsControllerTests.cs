using AngleSharp.Html.Dom;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using TravelBook.IntegrationTests.Helpers;
using Microsoft.AspNetCore.WebUtilities;

namespace TravelBook.IntegrationTests;

public class PhotoAlbumsControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("10")]
    public async Task Post_CreatePhotoalbum_ReturnRedirectToTravelPhotoAlbums(string travelId)
    {
        // Arrange
        string destPath = "/photoalbums/travelphotoalbums";
        var defaultPage = await AuthClientWithRedir.GetAsync($"/photoalbums/create?travelId={travelId}");
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        // Act
        var response = await AuthClientWithRedir.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            new Dictionary<string, string>
            {
                ["title"] = "in zoo",
            });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(destPath, response.RequestMessage.RequestUri.LocalPath);
    }

    [Theory]
    [InlineData("19", "/photoalbums/all")]
    public async Task Post_DeletePhotoalbum_ReturnRedirectToAllPhotoAlbums(string photoalbumId, string destPath)
    {
        // Arrange
        string url = QueryHelpers.AddQueryString("/photoalbums/delete",
            new Dictionary<string, string?>() { { "photoAlbumId", photoalbumId }, { "returnUrl", destPath } });

        var defaultPage = await AuthClientWithRedir.GetAsync(url);
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        // Act
        var response = await AuthClientWithRedir.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            (IHtmlButtonElement)content.QuerySelector("[type=submit]"));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(destPath, response.RequestMessage.RequestUri.LocalPath);
    }
}

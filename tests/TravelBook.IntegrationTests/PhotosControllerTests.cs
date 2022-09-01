using AngleSharp.Html.Dom;
using System.Net;
using TravelBook.IntegrationTests.Helpers;

namespace TravelBook.IntegrationTests;

public class PhotosControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData("69")]
    public async Task Post_EditPhoto_ReturnRedirectToPhotoAlbum(string photoId)
    {
        // Arrange
        string destPath = "/photoalbums/open";
        var defaultPage = await AuthClientWithRedir.GetAsync($"/photos/edit?photoId={photoId}");
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        // Act
        var response = await AuthClientWithRedir.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            new Dictionary<string, string>
            {
                ["Title"] = "Some title",
                ["Place"] = "Hotel",
            });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(destPath, response.RequestMessage.RequestUri.LocalPath);
    }
}

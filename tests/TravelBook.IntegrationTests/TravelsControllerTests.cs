using AngleSharp.Html.Dom;
using System.Net;
using TravelBook.IntegrationTests.Helpers;

namespace TravelBook.IntegrationTests;

public class TravelsControllerTests : BaseControllerTests
{
    [Fact]
    public async Task Post_CreateTravel_ReturnRedirectToAllTravels()
    {
        // Arrange
        string destPath = "/travels/all";
        var defaultPage = await AuthClientWithRedir.GetAsync("/travels/create");
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        var response = await AuthClientWithRedir.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            new Dictionary<string, string>
            {
                ["City"] = "Toronto",
                ["Country"] = "Canada",
                ["DateStartTravel"] = "2022-08-03",
                ["DateFinishTravel"] = "2022-08-18",
            });
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(destPath, response.RequestMessage.RequestUri.AbsolutePath);
    }

    [Theory]
    [InlineData("20")]
    public async Task Post_DeleteTravel_ReturnRedirectToAllTravels(string id)
    {
        // Arrange
        string destPath = "/travels/all";
        var defaultPage = await AuthClientWithRedir.GetAsync($"/travels/delete?travelId={id}");
        var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        var response = await AuthClientWithRedir.SendAsync(
            (IHtmlFormElement)content.QuerySelector("form"),
            (IHtmlButtonElement)content.QuerySelector("[type=submit]"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(destPath, response.RequestMessage.RequestUri.AbsolutePath);
    }
}

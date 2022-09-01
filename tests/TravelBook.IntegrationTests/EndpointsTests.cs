using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.WebUtilities;

namespace TravelBook.IntegrationTests;

public class EndpointsTests : BaseControllerTests
{
    [Theory]
    [MemberData(nameof(TravelEndpoints))]
    [MemberData(nameof(ArticleEndpoints))]
    [MemberData(nameof(PhotoAlbumEndpoints))]
    [MemberData(nameof(PhotoEndpoints))]
    [MemberData(nameof(AccountEndpoints))]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Act
        var response = await AuthClientWithRedir.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8",
            response.Content.Headers.ContentType.ToString());

    }

    [Theory]
    [MemberData(nameof(TravelEndpoints))]
    [MemberData(nameof(ArticleEndpoints))]
    [MemberData(nameof(PhotoAlbumEndpoints))]
    [MemberData(nameof(PhotoEndpoints))]
    [MemberData(nameof(AccountEndpoints))]
    public async Task Get_EndpointsReturnRedirectToLoginPage(string url)
    {
        // arrange
        string loginPagePath = "/account/login";
        var client = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        // Act
        var response = await client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal(loginPagePath, response.Headers.Location.LocalPath);
    }

    [Theory]
    [InlineData("/travels/delete?travelId=8")]
    [InlineData("/articles/edit?articleId=1")]
    [InlineData("/photoalbums/open?photoAlbumId=10")]
    [InlineData("/photos/edit?photoId=41")]
    public async Task Get_EndpointsReturnAccessDenied(string url)
    {
        // arrange
        string accessDeniedPagePath = "/error/403";

        // Act
        var response = await AuthClientWithRedir.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        Assert.Equal(accessDeniedPagePath, response.RequestMessage.RequestUri.AbsolutePath);
    }


    public static readonly IEnumerable<object[]> TravelEndpoints = new List<object[]>()
    {
        new object[] {"/travels/all"},
        new object[] {"/travels/create"},
        new object[] {QueryHelpers.AddQueryString("/travels/delete",
            new Dictionary<string, string?>() { { "travelId", "10" } }) },
    };

    public static readonly IEnumerable<object[]> ArticleEndpoints = new List<object[]>()
    {
        new object[] {"/articles/all"},

        new object[] {QueryHelpers.AddQueryString("/articles/travelarticles",
            new Dictionary<string, string?>() { { "travelId", "10" } }) },

        new object[] {QueryHelpers.AddQueryString("/articles/create",
            new Dictionary<string, string?>() { { "travelId", "10" } }) },

        new object[] {QueryHelpers.AddQueryString("/articles/edit",
            new Dictionary<string, string?>() { { "articleId", "11" } }) },

        new object[] {QueryHelpers.AddQueryString("/articles/delete",
            new Dictionary<string, string?>() { { "articleId", "13" } }) },
    };

    public static readonly IEnumerable<object[]> PhotoAlbumEndpoints = new List<object[]>()
    {
        new object[] {"/photoalbums/all"},

        new object[] {QueryHelpers.AddQueryString("/photoalbums/open",
            new Dictionary<string, string?>() { { "photoAlbumId", "14" } }) },

        new object[] {QueryHelpers.AddQueryString("/photoalbums/travelphotoalbums",
            new Dictionary<string, string?>() { { "travelId", "10" } }) },

        new object[] {QueryHelpers.AddQueryString("/photoalbums/uploadphotostoalbum",
            new Dictionary<string, string?>() { { "photoAlbumId", "14" } }) },

        new object[] {QueryHelpers.AddQueryString("/photoalbums/create",
            new Dictionary<string, string?>() { { "travelId", "10" } }) },
       
        new object[] {QueryHelpers.AddQueryString("/photoalbums/delete",
            new Dictionary<string, string?>() { { "photoAlbumId", "18" } }) },
    };

    public static readonly IEnumerable<object[]> PhotoEndpoints = new List<object[]>()
    {
        new object[] {QueryHelpers.AddQueryString("/photos/edit",
            new Dictionary<string, string?>() { { "photoId", "69" } }) },

        new object[] {QueryHelpers.AddQueryString("/photos/delete",
            new Dictionary<string, string?>() { { "photoId", "70" } }) },
    };

    public static readonly IEnumerable<object[]> AccountEndpoints = new List<object[]>()
    {
        new object[] {"/account/aboutme"},
        new object[] {"/account/logout"},
    };
}

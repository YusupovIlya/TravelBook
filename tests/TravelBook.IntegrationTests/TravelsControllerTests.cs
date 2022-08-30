using AngleSharp.Html.Dom;
//using JSNLog.Infrastructure;
using System.Net.Http.Headers;
using TravelBook.Web.Controllers;
using TravelBook.Web.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using JSNLog.Infrastructure;
using Microsoft.AspNetCore.WebUtilities;

namespace TravelBook.IntegrationTests;

public class TravelsControllerTests : BaseControllerTests
{

    public static readonly IEnumerable<object[]> Endpoints = new List<object[]>()
    {
        new object[] {"/travels/all"},
        new object[] {"/travels/create"},
        new object[] {QueryHelpers.AddQueryString("/travels/delete",
            new Dictionary<string, string?>() { { "travelId", "10" } }) },
    };



    [Theory]
    [MemberData(nameof(Endpoints))]
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
    [MemberData(nameof(Endpoints))]
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
    //private Task<Travel[]> GetAllUserTravels()
    //{
    //    Travel[] travels = new Travel[]
    //    {
    //        new Travel("3b62472e-4f66-49fa-a20f-e7685b9565d8", new Place("New York", "USA"),
    //        CreateRandomTravelPeriod().start, CreateRandomTravelPeriod().finish),

    //        new Travel("3b62472e-4f66-49fa-a20f-e7685b9565d8", new Place("Tokio", "Japan"),
    //        CreateRandomTravelPeriod().start, CreateRandomTravelPeriod().finish),

    //        new Travel("3b62472e-4f66-49fa-a20f-e7685b9565d8", new Place("Sidney", "Australia"),
    //        CreateRandomTravelPeriod().start, CreateRandomTravelPeriod().finish),
    //    };
    //    return Task.FromResult(travels);
    //}
    //private (DateTime start, DateTime finish) CreateRandomTravelPeriod()
    //{
    //    Random rand = new Random();
    //    int dayStart = rand.Next(1, 15);
    //    int dayFinish = dayStart + rand.Next(1, 11);
    //    var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayStart);
    //    var finishDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayFinish);
    //    return (startDate, finishDate);
    //}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelBook.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

namespace TravelBook.IntegrationTests;

public class BaseControllerTests
{
    //public void Y()
    //{
    //    var _userRepository = new Mock<IUserRepository>();
    //    var _userStore = new Mock<IUserStore<IdentityUser>>();
    //    var userManager = new UserManager<IdentityUser>(_userStore.Object);
    //    var applicationUserManager = new ApplicationUserManager(_userStore.Object);
    //    var _countersIdRepository = new Mock<ICountersIdRepository>();
    //    var _userService = new Mock<IUserService>();

    //    RegisterBindingModel registerModel = new RegisterBindingModel() { Email = "test@gmail.com", Password = "qweasd" };
    //    IdentityUser appUser = new IdentityUser("TestName");
    //    UserService userService = new UserService(_userRepository.Object, _countersIdRepository.Object);

    //    _userStore.Setup(x => x.CreateAsync(appUser, CancellationToken.None)).Returns(Task.FromResult(IdentityResult.Success));
    //    _countersIdRepository.Setup(x => x.GetUserLastId()).Returns(10);
    //    _userService.Setup(x => x.Register(registerModel)).Returns(Task.FromResult(IdentityResult.Success));

    //    userService.UserManager = applicationUserManager;
    //}
    protected BaseControllerTests()
    {
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");        
            });

        AuthClientWithOutRedir = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        AuthClientWithOutRedir.DefaultRequestHeaders.Add("TestWithAuth", "true");

        AuthClientWithRedir = Factory.CreateClient();
        AuthClientWithRedir.DefaultRequestHeaders.Add("TestWithAuth", "true");
    }

    protected WebApplicationFactory<Program> Factory { get; }
    protected HttpClient AuthClientWithRedir { get; }
    protected HttpClient AuthClientWithOutRedir { get; }
}

using System.Security.Claims;

namespace TravelBook.Web.Service;

public class AuthenticatedTestRequestMiddleware
{
	public const string TestingHeader = "TestWithAuth";
	public const string TestingHeaderValue = "true";

	private readonly RequestDelegate _next;

	public AuthenticatedTestRequestMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
        if (context.Request.Headers.Keys.Contains(TestingHeader) &&
            context.Request.Headers[TestingHeader].Contains(TestingHeaderValue))
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>
                        {
                    new Claim(ClaimTypes.NameIdentifier, "0cc8c7eb-b26c-4c51-874c-b21b7cd9e073"),
                    new Claim(ClaimTypes.Name, "asds@gmail.com"),
                    new Claim(ClaimTypes.Email, "asds@gmail.com"),
                        }, "Identity.Application");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            context.User = claimsPrincipal;
        }
        await _next(context);
	}
}

using TravelBook.Infrastructure.Repositories;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(AppDbContext))));
builder.Services.AddScoped<ITravelRepository, TravelRepository>();
builder.Services.AddScoped<IPhotoAlbumRepository, PhotoAlbumRepository>();
builder.Services.AddScoped<IFilesService, FilesService>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
    opts.SignIn.RequireConfirmedAccount = false;
    opts.SignIn.RequireConfirmedPhoneNumber = false;
    opts.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "TravelBookAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/error/403";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Account}/{action=Login}/{id?}");
});

app.Run();

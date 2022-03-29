using FazendaFeliz.Infrastructure.Configurations;
using FazendaFeliz.Infrastructure.Data.Context;
using FazendaFeliz.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.UI;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.

var configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

#if DEBUG
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
#else
builder.Services.AddControllersWithViews();
#endif

builder.Services.AddResponseCompression();
/*builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));*/

builder.Services.AddControllersWithViews()
        .AddMicrosoftIdentityUI();

string mySQLConnectionString = configuration.GetConnectionString("MySqlConnectionString");

builder.Services.AddDbContext<AppDbContext>(optionsBuilder => optionsBuilder.UseMySql(
    mySQLConnectionString, ServerVersion.AutoDetect(mySQLConnectionString)));

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None; // To prevent iOS SSO infinite redirect with Lax cookies (default)
});

builder.Services.AddInfrastructure();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

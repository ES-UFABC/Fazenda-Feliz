using FazendaFeliz.Infrastructure.Configurations;
using FazendaFeliz.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("Identity.Login")
    .AddCookie("Identity.Login", config =>
    {
        config.Cookie.Name = "Identity.Login";
        config.LoginPath = "/usuario/login";
        config.AccessDeniedPath = "/usuario/criar";
        config.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

string mySQLConnectionString = configuration.GetConnectionString("MySqlConnectionString");

builder.Services.AddDbContext<AppDbContext>(optionsBuilder => optionsBuilder.UseMySql(
    mySQLConnectionString, ServerVersion.AutoDetect(mySQLConnectionString)));

builder.Services.AddHttpContextAccessor();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

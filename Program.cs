using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using PROG7312_P1_V1.Database;
using PROG7312_P1_V1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the service files
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ReportIssueService>();

// Register the in-memory database
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseInMemoryDatabase("TestDb"));

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Languages");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("af-ZA"),
        new CultureInfo("zu-ZA"),
        new CultureInfo("xh-ZA"),
        new CultureInfo("st-ZA"),
        new CultureInfo("tn-ZA"),
        new CultureInfo("nr-ZA"),
        new CultureInfo("nso-ZA"),
        new CultureInfo("ve-ZA"),
        new CultureInfo("ts-ZA"),
        new CultureInfo("ss-ZA"),
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    // Make cookie provider the first (highest priority)
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});


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

var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

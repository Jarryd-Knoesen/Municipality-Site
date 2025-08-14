using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using PROG7312_P1_V1.Database;
using PROG7312_P1_V1.Services;
using PROG7312_P1_V1.DataModels;

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

// Inserting test data into the in-memory database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    //checks if the DB is ready
    context.Database.EnsureCreated();

    // Load images from the wwwroot/images directory
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

    // Images for the issue reports
    var imagePath1 = Path.Combine(env.WebRootPath, "images", "pothole.png");
    var imagePath2 = Path.Combine(env.WebRootPath, "images", "error.png");
    byte[] imageBytes1 = File.Exists(imagePath1) ? File.ReadAllBytes(imagePath1) : new byte[] { 0x0 };
    byte[] imageBytes2 = File.Exists(imagePath2) ? File.ReadAllBytes(imagePath2) : new byte[] { 0x0 };

    // Images for the events
    var eventImagePath1 = Path.Combine(env.WebRootPath, "images", "Warrior.jpg");
    var eventImagePath2 = Path.Combine(env.WebRootPath, "images", "comic.jpg");
    var eventImagePath3 = Path.Combine(env.WebRootPath, "images", "portuguese.jpg");
    byte[] eventImageBytes1 = File.Exists(eventImagePath1) ? File.ReadAllBytes(eventImagePath1) : new byte[] { 0x0 };
    byte[] eventImageBytes2 = File.Exists(eventImagePath2) ? File.ReadAllBytes(eventImagePath2) : new byte[] { 0x0 };
    byte[] eventImageBytes3 = File.Exists(eventImagePath3) ? File.ReadAllBytes(eventImagePath3) : new byte[] { 0x0 };

    // Inserts Profile Data
    if (!context.Profiles.Any())
    {
        context.Profiles.AddRange(
            new Profile 
            { 
                ProfileID = "P1", 
                FullName = "John Dow", 
                Email = "johndoe@example.com", 
                Password = "Password123", 
                PhoneNumber = "0123456789" 
            },
            new Profile 
            { 
                ProfileID = "P2", 
                FullName = "Jane Smith", 
                Email = "janesmith@example.com", 
                Password = "Password123", 
                PhoneNumber = "0987654321" 
            }
        );
    }

    // Inserts Issue Report Data
    if (!context.IssueReports.Any())
    {
        context.IssueReports.AddRange(
            new IssueReport
            {
                IssueId = "I1",
                Category = "Road",
                Location = "Main Street",
                Description = "Large pothole neat the post office",
                ImageBytes = imageBytes1,
                ReportedDate = DateTime.Now
            },
            new IssueReport
            {
                IssueId = "I2",
                Category = "Streetlight",
                Location = "5th Avenue",
                Description = "Streetlight dissappeared",
                ImageBytes = imageBytes2,
                ReportedDate = DateTime.Now
            }
        );
    }

    // Inserts Announcements Data
    if (!context.Announcements.Any())
    {
        context.Announcements.AddRange(
            new Announcements
            {
                AnnouncementId = "A1",
                Title = "Road Maintenance",
                Message = "Scheduled road maintenance on Main Street next week.",
                DatePosted = new DateTime(2025, 08, 05)
            },
            new Announcements
            {
                AnnouncementId = "A2",
                Title = "Streetlight Repair",
                Message = "Streetlight repairs will be conducted on 5th Avenue.",
                DatePosted = new DateTime(2025, 08, 12)
            }
        );

    }

    // Inserts Events Data
    if (!context.Events.Any())
    {
        
        context.Events.AddRange(
            new Events
            {
                EventId = "E1",
                Title = "Warrior Race",
                Description = "Join us for a blood pumping race!!!",
                DatePosted = new DateTime(2025, 08, 01
                ),
                EventDate = new DateTime(2025, 08, 15, 9, 0, 0),
                Location = "Krugerdorp",
                ImageBytes = eventImageBytes1
            },
            new Events
            {
                EventId = "E2",
                Title = "Comic Con",
                Description = "A day of fun and games for all ages.",
                DatePosted = new DateTime(2025, 08, 02),
                EventDate = new DateTime(2025, 08, 20, 10, 0, 0),
                Location = "Johannesburg",
                ImageBytes = eventImageBytes2
            },
            new Events
            {
                EventId = "E3",
                Title = "Portuguese Festival",
                Description = "Experience the culture and cuisine of Portugal.",
                DatePosted = new DateTime(2025, 08, 03),
                EventDate = new DateTime(2025, 08, 25, 11, 0, 0),
                Location = "Cape Town",
                ImageBytes = eventImageBytes3
            }
        );
    }

    context.SaveChanges();
}

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
    pattern: "{controller=LanguageSelect}/{action=LanguageSelect}/{id?}");

app.Run();

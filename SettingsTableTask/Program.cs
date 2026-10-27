using Microsoft.EntityFrameworkCore;
using SettingsTableTask.Components;
using SettingsTableTask.Data;
using SettingsTableTask.Models;
using SettingsTableTask.Services;

var builder = WebApplication.CreateBuilder(args);

// =====================
// Services
// =====================

// Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Database (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=settings.db"));

// Services
builder.Services.AddScoped<SettingService>();


var app = builder.Build();

// =====================
// Middleware
// =====================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// 404 Page
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseAntiforgery();

// Static files
app.MapStaticAssets();

// Blazor App
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!db.Settings.Any())
    {
        db.Settings.AddRange(
            new Setting
            {
                SettingKey = "SiteTitle",
                SettingValue = "Shahd's Web",
                Description = "Website title",
                CreatedOn = DateTime.Now
            },
            new Setting
            {
                SettingKey = "AdminEmail",
                SettingValue = "shahdalawneh0@gmail.com",
                Description = "Admin email",
                CreatedOn = DateTime.Now
            },
            new Setting
            {
                SettingKey = "Theme",
                SettingValue = "Light",
                Description = "Application theme",
                CreatedOn = DateTime.Now
            }
        );

        db.SaveChanges();
    }
}

app.Run();
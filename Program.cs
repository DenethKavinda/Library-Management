using Microsoft.AspNetCore.Authentication.Cookies; // 💡 NEW IMPORT
using Microsoft.EntityFrameworkCore;
using SarasaviLibrary.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Registered EF Core Database Context for SarasaviLibraryDB connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 💡 1. REGISTER COOKIE AUTHENTICATION SERVICES WITH ROLE AUTHORIZATION POLICIES
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/"; // Redirect target if an unauthenticated user hits a protected route
        options.AccessDeniedPath = "/"; // Redirect target if a standard User tries to access Librarian pages
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// 💡 2. ADD MIDDLEWARE IN THIS EXACT CRITICAL ORDER
app.UseAuthentication(); // Who are you?
app.UseAuthorization();  // What are you allowed to see?

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
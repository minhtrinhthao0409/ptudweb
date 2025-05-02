using BackendHtml.Context;
using BackendHtml.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AIRepository>();

//Register

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    p =>
    {
        p.LoginPath = "/account/login";
        p.AccessDeniedPath = "/account/denied";
        p.Cookie.Name = "MyAppAuth";
        p.ExpireTimeSpan = TimeSpan.FromDays(30);
    }
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}





//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // Add this line to enable authentication middleware
app.UseAuthorization(); // Add this line to enable authorization middleware

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");


app.Run();

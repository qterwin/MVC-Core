using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using mvccore.Abstraction;
using mvccore.Context;

var builder = WebApplication.CreateBuilder(args);

//Service Registration
builder.Services.AddSingleton<ISqlConnection, SqlConnectionService>();
// 

// Add services to the container.
builder.Services.AddControllersWithViews();

//Book Connection
builder.Services.AddDbContext<BookContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("BookConnection"));
});

builder.Services.AddDbContext<CentralAccessContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("LogConnection"));
});

// Add automapper configuration
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Auth 
builder.Services.AddAuthentication(
                 CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(option =>
                    {
                        option.LoginPath = "/Login/Login";
                        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                        option.AccessDeniedPath = "/Login/Login";
                    });

builder.Services.AddAuthorization(option =>
                           {
                               option.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
